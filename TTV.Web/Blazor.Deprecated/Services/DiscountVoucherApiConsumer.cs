using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class DiscountVoucherApiConsumer(HttpClient httpClient) : IDiscountVoucherApiConsumer
{
    private readonly HttpClient httpClient = httpClient;
    private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<DiscountVoucherDto> IssueAsync(DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("discount-vouchers", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DiscountVoucherDto>(
            responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<DiscountVoucherDto[]> GetListAsync(bool claimedByCurrentUser = false, CancellationToken cancellationToken = default)
    {

        return await httpClient.GetFromJsonAsync<DiscountVoucherDto[]>($"discount-vouchers/{(claimedByCurrentUser ? "own" : "")}", cancellationToken) ?? [];
    }

    public async Task<ResultDto<OrderDto?>> ApplyDiscountVoucherAsync(string voucherCode, int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PutAsync($"discount-vouchers/{voucherCode}/order/{orderId}", null, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<OrderDto?>>(
            responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }
}
