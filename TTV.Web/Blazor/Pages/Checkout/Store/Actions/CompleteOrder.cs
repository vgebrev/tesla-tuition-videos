using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record CompleteOrderRequest
{
    public int OrderId { get; init; }
}

public record CompleteOrderResponse
{
    public ResultDto<OrderDto> Result { get; init; } = new(new(), false, null);
}

public record CompleteOrderError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
