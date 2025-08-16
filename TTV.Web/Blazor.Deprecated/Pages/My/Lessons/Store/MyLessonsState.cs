using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Lessons.Store;

[FeatureState]
public record MyLessonsState
{
    public LessonDto[]? Lessons { get; init; }
    public bool IsLoading { get; init; }
    public ErrorState Error { get; init; } = new();
}
