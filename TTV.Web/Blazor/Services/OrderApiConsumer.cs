using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class OrderApiConsumer(HttpClient httpClient) : IOrderApiConsumer
{
    private readonly HttpClient httpClient = httpClient;
    private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<OrderDto> CreateNewOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("orders", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<OrderDto>(responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<OrderDto[]> GetOwnListAsync(CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"orders/own", cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<OrderDto[]>(responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");

    }

    public async Task<OrderDto?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"orders/{orderId}", cancellationToken);
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<OrderDto>(responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<ResultDto<OrderDto>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PutAsync($"orders/{orderId}/complete", null, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<OrderDto>>(
            responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<ResultDto<OrderDto>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.DeleteAsync($"orders/{orderId}", cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ResultDto<OrderDto>>(
            responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }
}
