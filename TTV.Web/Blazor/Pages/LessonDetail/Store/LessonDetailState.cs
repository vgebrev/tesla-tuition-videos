using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store;

[FeatureState]
public record LessonDetailState
{
    public bool IsLoading => IsLessonLoading || IsDocumentsLoading;
    public bool IsLessonLoading { get; init; }
    public bool IsDocumentsLoading { get; init; }
    public LessonDto? Lesson { get; init; }
    public DocumentDto[] Documents { get; init; } = [];
    public ErrorState Error { get; init; } = new();
}
