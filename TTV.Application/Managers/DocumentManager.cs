using TTV.Application.Exceptions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public class DocumentManager(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentity) : IDocumentManager
{
    public async Task<IEnumerable<Document>> GetDocumentsForLessonOwnedByUserAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId;
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var documents = await unitOfWork.DocumentRepository.GetDocumentsForLessonOwnedByUserAsync(lessonId, userId, cancellationToken);
        return documents;
    }

    public async Task<Document?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken)
    {
        var userId = userIdentity.UserId;
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var document = await unitOfWork.DocumentRepository.GetDocumentOwnedByUserAsync(documentId, userId, cancellationToken);
        return document;
    }

}
