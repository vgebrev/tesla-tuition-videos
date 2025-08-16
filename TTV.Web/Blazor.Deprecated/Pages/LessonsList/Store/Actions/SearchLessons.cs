using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store.Actions;

public record SearchLessonsRequest
{
    public string? SearchText { get; init; }
    public TagDto[]? SearchTags { get; init; }
}

public record SearchLessonsResponse
{
    public LessonDto[] Lessons { get; init; } = [];
}

public record SearchLessonsError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
