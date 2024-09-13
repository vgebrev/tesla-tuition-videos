using TTV.Application.Payments;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Payments;
public class ManualBankTransferPaymentProcessor : IPaymentProcessor
{
    public async Task<Result<Payment>> InitiateAsync(Order order, CancellationToken cancellationToken = default)
    {
        var id = Guid.NewGuid();
        var payment = new Payment()
        {
            Id = id,
            ExternalIdentifier = id.ToString(),
            CreatedOn = DateTime.Now,
            CreatedBy = order.PlacedBy,
            Amount = order.TotalAmount,
            Order = order,
            Status = PaymentStatus.Pending,
            Type = PaymentType.ManualBankTransfer
        };
        order.Payments.Add(payment);
        return await Task.FromResult(new Result<Payment>(payment, true, $"New {PaymentType.ManualBankTransfer.ToDisplayString()} initiated."));
    }

    public async Task<Result> ConfirmAsync(Payment payment, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        var confirmation = new PaymentConfirmation()
        {
            ReceivedOn = DateTime.Now,
            Payment = payment,
            IsSuccessful = true
        };
        return await Task.FromResult(payment.Confirm(confirmation));
    }
}
