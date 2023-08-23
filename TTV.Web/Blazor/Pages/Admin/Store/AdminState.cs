using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Admin.Store;

[FeatureState]
public record AdminState
{
    public bool IsLoading { get; init; }
    public ErrorState Error { get; init; } = new();
    public DiscountVoucherDto[]? DiscountVouchers { get; init; }
    public DiscountVoucherDto? IssuedVoucher { get; init; }
}
