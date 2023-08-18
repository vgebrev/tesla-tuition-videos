using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;

public record RemoveLesson
{
    public LessonDto Lesson { get; init; } = new();
}
