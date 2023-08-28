using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store;

[FeatureState]
public record CheckoutState
{
    public bool IsLoading { get; init; }
    public string VoucherCode { get; init; } = string.Empty;
    public ResultDto<AppliedDiscountDto?>? ApplyVoucherResult { get; init; }
    public ResultDto<OrderDto>? CompleteOrderResult { get; init; }
    public ResultDto<OrderDto>? CancelOrderResult { get; init; }
    public OrderDto? Order { get; init; }
    public ErrorState Error { get; init; } = new();
}
