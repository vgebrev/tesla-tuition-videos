using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Domain.DomainServices.Repositories;

public interface ILessonRepository
{
    Task<Page<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, Guid? ownerId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Lesson?> GetByIdAsync(int lessonId, Guid? ownerId, CancellationToken cancellationToken = default);
    Task<Page<Lesson>> GetLessonsOwnedByUserAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(Guid userId, int[] lessonsIds, CancellationToken cancellationToken = default);
}
