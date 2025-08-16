using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store.Actions;

public record GetLessonRequest
{
    public int LessonId { get; set; }
}

public record GetLessonResponse
{
    public LessonDto? Lesson { get; init; } = new();
}

public record GetLessonError
{
    public string ErrorMessage { get; init; } = string.Empty;
}