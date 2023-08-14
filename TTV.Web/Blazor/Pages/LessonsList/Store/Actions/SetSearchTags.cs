using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store.Actions;

public record SetSearchTags
{
    public TagDto[]? SearchTags { get; init; }
}
