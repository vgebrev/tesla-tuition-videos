using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lessons.Actions;

public record SetSearchTags
{
    public TagDto[]? SearchTags { get; init; }
}
