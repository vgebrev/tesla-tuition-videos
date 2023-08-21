using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

[FeatureState]
public record ShoppingCartState
{
    public bool IsLoading { get; init; } = false;
    public LessonDto[] Lessons { get; init; } = Array.Empty<LessonDto>();
    public ErrorState Error { get; init; } = new();
}
