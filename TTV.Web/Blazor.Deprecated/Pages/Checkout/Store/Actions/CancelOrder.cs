using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record CancelOrderRequest
{
    public int OrderId { get; init; }
}

public record CancelOrderResponse
{
    public ResultDto<OrderDto> Result { get; init; } = new(new(), false, null);
}

public record CancelOrderError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
