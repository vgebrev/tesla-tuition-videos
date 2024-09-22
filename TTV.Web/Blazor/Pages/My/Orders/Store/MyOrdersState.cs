using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Orders.Store;

[FeatureState]
public record MyOrdersState
{
    public bool IsLoading { get; init; }
    public ErrorState Error { get; init; } = new();
    public OrderDto[]? Orders { get; init; }
}