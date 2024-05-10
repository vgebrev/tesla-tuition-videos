using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface IDocumentManager
{
    Task<IEnumerable<Document>> GetDocumentsForLessonOwnedByUserAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<Document?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken);
}
