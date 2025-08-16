using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store.Actions;

public record GetTagsRequest
{
}

public record GetTagsResponse
{
    public TagDto[] Tags { get; init; } = [];
}

public record GetTagsError
{
    public string ErrorMessage { get; init; } = string.Empty;
}

