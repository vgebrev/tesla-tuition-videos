using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Lessons.Store.Actions;

public record GetLessonsRequest
{
}

public record GetLessonsResponse
{
    public LessonDto[] Lessons { get; init; } = [];
}

public record GetLessonsError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
