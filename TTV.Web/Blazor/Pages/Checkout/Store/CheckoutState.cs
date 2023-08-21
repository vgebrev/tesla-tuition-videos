using Fluxor;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.Checkout.Store;

[FeatureState]
public record CheckoutState
{
    public bool IsLoading { get; init; }
    public ErrorState Error { get; init; } = new();
}
