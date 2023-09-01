using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.DiscountVouchers.Store;

[FeatureState]
public record MyDiscountVouchersState
{
    public bool IsLoading { get; init; }
    public ErrorState Error { get; init; } = new();
    public DiscountVoucherDto[]? DiscountVouchers { get; init; }
}