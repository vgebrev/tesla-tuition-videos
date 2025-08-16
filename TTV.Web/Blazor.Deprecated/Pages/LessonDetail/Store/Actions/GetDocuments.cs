using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store.Actions;

public record GetDocumentsRequest
{
    public int LessonId { get; set; }
}

public record GetDocumentsResponse
{
    public DocumentDto[] Documents { get; init; } = [];
}

public record GetDocumentsError
{
    public string ErrorMessage { get; init; } = string.Empty;
}