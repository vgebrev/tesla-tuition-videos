using Fluxor;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store;

[FeatureState]
public record LessonsListState
{
    public bool IsLoadingLessons { get; init; }
    public LessonDto[]? Lessons { get; init; }
    public ErrorState LessonsError { get; init; } = new();

    public bool IsLoadingTags { get; init; }
    public TagDto[]? Tags { get; init; }
    public ErrorState TagsError { get; init; } = new();

    public string? SearchText { get; init; }
    public TagDto[]? SearchTags { get; init; }
}
