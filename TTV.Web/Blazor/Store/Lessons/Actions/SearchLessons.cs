using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lessons.Actions;

public record SearchLessonsRequest
{
    public string? SearchText { get; init; }
    public TagDto[]? SearchTags { get; init; }
}

public record SearchLessonsResponse
{
    public LessonDto[] Lessons { get; init; } = Array.Empty<LessonDto>();
}
