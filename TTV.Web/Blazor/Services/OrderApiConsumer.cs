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

    public async Task<OrderDto?> GetOrderAsync(int orderId)
    {
        var httpResponse = await httpClient.GetAsync($"api/order/{orderId}");
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }
}
