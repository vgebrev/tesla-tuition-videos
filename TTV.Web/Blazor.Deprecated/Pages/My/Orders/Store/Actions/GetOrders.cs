using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Orders.Store.Actions;

public record GetOrdersRequest
{
}

public record GetOrdersResponse
{
    public OrderDto[] Orders { get; init; } = [];
}

public record GetOrdersError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
