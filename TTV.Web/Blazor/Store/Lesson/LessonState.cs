using Fluxor;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lesson;

[FeatureState]
public record LessonState
{
    public bool IsLoading { get; init; }
    public LessonDto? Lesson { get; init; } = null;
    public ErrorState Error { get; init; } = new();
}
