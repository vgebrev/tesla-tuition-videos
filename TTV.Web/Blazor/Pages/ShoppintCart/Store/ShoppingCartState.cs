using Fluxor;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

[FeatureState]
public record ShoppingCartState
{
    public LessonDto[] Lessons { get; init; } = Array.Empty<LessonDto>();
}
