using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record GetOrderRequest
{
    public int OrderId { get; set; }

    public OrderDto? CurrentOrder { get; init; }
}

public record GetOrderResponse
{
    public OrderDto? Order { get; init; }
}

public record GetOrderError
{
    public string ErrorMessage { get; init; } = string.Empty;
}