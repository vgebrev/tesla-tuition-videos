using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class PaymentApiConsumer : IPaymentApiConsumer
{
    private readonly HttpClient httpClient;

    public PaymentApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ResultDto<PaymentDto?>> InitiateOrderPaymentAsync(PaymentInitiateDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/payment", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<PaymentDto?>>(
            responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }
}
