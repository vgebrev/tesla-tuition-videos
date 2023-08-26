using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.OrderComplete.Store;

[FeatureState]
public record OrderCompleteState
{
    public bool IsLoading { get; init; }
    public OrderDto? Order { get; init; }
    public ErrorState Error { get; init; } = new();
}
