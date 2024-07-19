using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.Lessons.Store.Actions;

public record AddLessons
{
    public LessonDto[]? Lessons { get; init; }
}
