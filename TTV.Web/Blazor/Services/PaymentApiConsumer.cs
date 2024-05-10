using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class PaymentApiConsumer(HttpClient httpClient) : IPaymentApiConsumer
{
    private readonly HttpClient httpClient = httpClient;
    private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<ResultDto<PaymentDto?>> InitiateOrderPaymentAsync(PaymentInitiateDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/payment", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<PaymentDto?>>(
            responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }
}
