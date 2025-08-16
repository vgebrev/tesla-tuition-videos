using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store.Actions;

public record UpdateLessons
{
    public LessonDto[]? Lessons { get; init; }
}
