using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;
public class DocumentRepository(DataContext dataContext) : IDocumentRepository
{
    public async Task<IEnumerable<Document>> GetDocumentsForLessonOwnedByUserAsync(int lessonId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Documents.TagWithCallSite()
            .Include(document => document.Lessons.Where(lesson => lesson.Id == lessonId))
            .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Where(document => document.Lessons.Any(lesson => lesson.Id == lessonId && lesson.OwnedBy.Any(user => user.Id == userId)))
            .ToListAsync(cancellationToken);
    }

    public async Task<Document?> GetDocumentOwnedByUserAsync(int documentId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Documents.TagWithCallSite()
            .Include(document => document.Lessons)
            .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .SingleOrDefaultAsync(document => document.Id == documentId && document.Lessons.Any(lesson => lesson.OwnedBy.Any(user => user.Id == userId)), cancellationToken);
    }

}
