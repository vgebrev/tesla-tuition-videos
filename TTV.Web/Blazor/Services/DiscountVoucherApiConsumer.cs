using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class DiscountVoucherApiConsumer : IDiscountVoucherApiConsumer
{
    private readonly HttpClient httpClient;

    public DiscountVoucherApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<DiscountVoucherDto> IssueAsync(DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/discount-voucher", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DiscountVoucherDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<DiscountVoucherDto[]> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<DiscountVoucherDto[]>("api/discount-voucher", cancellationToken) ?? Array.Empty<DiscountVoucherDto>();
    }
}
