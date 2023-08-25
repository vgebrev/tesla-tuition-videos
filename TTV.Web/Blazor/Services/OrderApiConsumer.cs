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

    public async Task<OrderDto> CreateNewOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/order", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<OrderDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<OrderDto?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"api/order/{orderId}", cancellationToken);
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<OrderDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<ResultDto<OrderDto>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PutAsync($"api/order/{orderId}/complete", null, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<OrderDto>>(
            responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }
}
