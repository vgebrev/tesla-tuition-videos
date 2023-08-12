using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lessons.Actions;

public record GetTagsRequest
{
}

public record GetTagsResponse
{
    public TagDto[] Tags { get; init; } = Array.Empty<TagDto>();
}

