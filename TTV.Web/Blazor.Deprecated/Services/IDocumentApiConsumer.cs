using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface IDocumentApiConsumer
{
    Task<DocumentDto[]> GetDocumentsForLessonOwnedByCurrentUserAsync(int lessonId, CancellationToken cancellationToken = default);
}
