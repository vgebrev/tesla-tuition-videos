using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Orders.Actions;

public record GetOrdersRequest
{
}

public record GetOrdersResponse
{
    public OrderDto[] Orders { get; init; } = Array.Empty<OrderDto>();
}

public record GetOrdersError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
