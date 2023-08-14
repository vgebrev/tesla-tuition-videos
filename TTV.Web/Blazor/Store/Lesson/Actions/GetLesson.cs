using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lesson.Actions;

public record GetLessonRequest
{
    public int LessonId { get; set; }
}

public record GetLessonResponse
{
    public LessonDto Lesson { get; init; } = default!;
}

public record GetLessonError
{
    public string ErrorMessage { get; init; } = string.Empty;
}