using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store;

[FeatureState]
public record LessonDetailState
{
    public bool IsLoading { get; init; }
    public LessonDto? Lesson { get; init; }
    public ErrorState Error { get; init; } = new();
}
