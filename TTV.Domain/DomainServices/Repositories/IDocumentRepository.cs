using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;
public interface IDocumentRepository
{
    Task<IEnumerable<Document>> GetDocumentsForLessonOwnedByUserAsync(int lessonId, Guid userId, CancellationToken cancellationToken = default);

    Task<Document?> GetDocumentOwnedByUserAsync(int documentId, Guid userId, CancellationToken cancellationToken = default);
}
