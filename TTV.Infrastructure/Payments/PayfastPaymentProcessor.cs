using System.Text.Json;
using TTV.Application;
using TTV.Application.Payments;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Payments;
public class PayfastPaymentProcessor : IPaymentProcessor
{
    private record PayfastTransactionIdentifier(string UUID);

    private readonly PayfastSettings settings;
    private readonly IHttpClientFactory httpClientFactory;

    public PayfastPaymentProcessor(PayfastSettings config, IHttpClientFactory httpClientFactory)
    {
        settings = config;
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<Result<Payment>> InitiateAsync(Order order, CancellationToken cancellationToken = default)
    {
        var payment = new Payment()
        {
            Id = Guid.NewGuid(),
            CreatedOn = DateTime.Now,
            CreatedBy = order.PlacedBy,
            Amount = order.TotalAmount,
            Order = order,
            Status = PaymentStatus.Pending,
            Type = PaymentType.Payfast
        };
        order.Payments.Add(payment);

        payment.ExternalIdentifier = await GetPayfastIdentifierAsync(payment, cancellationToken);

        return new Result<Payment>(payment, true, $"New {PaymentType.Payfast.ToDisplayString()} initiated.");
    }

    private async Task<string> GetPayfastIdentifierAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        var order = payment.Order;
        if (string.IsNullOrEmpty(order.PlacedBy.Email))
        {
            throw new InvalidOperationException("Cannot initiate payment without a valid email address.");
        }

        Dictionary<string, string> paymentData = new()
        {
            { "merchant_id", settings.MerchantId },
            { "merchant_key", settings.MerchantKey },
            { "return_url", $"{settings.ReturnUrl}/{order.Id}" },
            { "cancel_url", $"{settings.CancelUrl}/{order.Id}" },
            { "notify_url", settings.NotifyUrl },
            { "email_address", order.PlacedBy.Email },
            { "m_payment_id", payment.Id.ToString() },
            { "amount", order.TotalAmount.ToString("F") },
            { "item_name", $"Order#{order.Id}" },
        };

        var signature = GenerateSignature(paymentData, settings.Passphrase);
        paymentData.Add("signature", signature);

        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.PostAsync(settings.PayfastUrl, new FormUrlEncodedContent(paymentData), cancellationToken);
        response.EnsureSuccessStatusCode();
        using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var result = await JsonSerializer.DeserializeAsync<PayfastTransactionIdentifier>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken: cancellationToken);
        return result?.UUID ?? throw new InvalidOperationException("Payfast did not return a valid response.");
    }

    private static string DataToString(Dictionary<string, string> data)
    {
        return string.Join("&", data.Keys.Select(k => $"{k}={Uri.EscapeDataString(data[k])}"));
    }

    private static string GenerateSignature(Dictionary<string, string> data, string? passphrase = null)
    {
        var signature = DataToString(data);
        if (!string.IsNullOrEmpty(passphrase))
            signature += $"&passphrase={passphrase}";
        return signature.ToMD5Hash();
    }

    public async Task<Result> ConfirmAsync(Payment payment, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        var confirmation = new PayfastPaymentConfirmation()
        {
            ReceivedOn = DateTime.Now,
            Payment = payment,
            Payload = DataToString(confirmationData.Where(kvp => kvp.Key != "pf_host").ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? string.Empty)),
            PayfastPaymentId = int.Parse(confirmationData["pf_payment_id"]),
            PaymentStatus = confirmationData["payment_status"],
            ItemName = confirmationData["item_name"],
            AmountGross = decimal.Parse(confirmationData["amount_gross"]),
            AmountFee = decimal.Parse(confirmationData["amount_fee"]),
            AmountNet = decimal.Parse(confirmationData["amount_net"]),
            Signature = confirmationData["signature"],
        };

        var validationResult = await ValidateConfirmationAsync(confirmation, confirmationData, cancellationToken);
        confirmation.IsSuccessful = validationResult.IsSuccess;
        confirmation.FailureReason = validationResult.Message;

        return payment.Confirm(confirmation);
    }

    private async Task<Result> ValidateConfirmationAsync(PayfastPaymentConfirmation confirmation, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        try
        {
            var paymentData = confirmationData
            .Where(kvp => kvp.Key != "signature" && kvp.Key != "pf_host")
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? string.Empty);

            // Check 1 - Signature
            var signature = GenerateSignature(paymentData, settings.Passphrase);
            if (signature != confirmation.Signature)
            {
                return new Result(false, $"Signature mismatch. Received: {confirmation.Signature}, Computed: {signature}.");
            }

            // Check 2 - Allowed Payfast Hosts
            var host = confirmationData["pf_host"];
            if (!settings.AllowedHosts.Contains(host))
            {
                return new Result(false, $"Request host '{host}' not in allowed hosts list.");
            }

            // Check 3 - Payment Amount
            if (confirmation.AmountGross != confirmation.Payment.Amount)
            {
                return new Result(false, $"Amount mistmatch. Received: {confirmation.AmountGross}, Expected: {confirmation.Payment.Amount}");
            }

            // Check 4 - Validate with Payfast
            paymentData.Add("signature", confirmationData["signature"]);
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(settings.PayfastValidationUrl, new FormUrlEncodedContent(paymentData), cancellationToken);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            if (responseContent != "VALID")
            {
                return new Result(false, $"Server validation failed. Received response: {responseContent}");
            }
        }
        catch (Exception ex)
        {
            return new Result(false, $"Unexpected error: {ex.Message}");
        }

        return new Result(true);
    }
}
