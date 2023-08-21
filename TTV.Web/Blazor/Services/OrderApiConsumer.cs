using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class OrderApiConsumer : IOrderApiConsumer
{
    private readonly HttpClient httpClient;

    public OrderApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<OrderDto> CreateNewOrderAsync(OrderCreateDto dto)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/order", dto);
        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }
}
