using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface ILessonRepository
{
    Task<IEnumerable<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, Guid? ownerId, CancellationToken cancellationToken = default);
    Task<Lesson?> GetByIdAsync(int lessonId, Guid? ownerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lesson>> GetLessonsOwnedByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(Guid userId, int[] lessonsIds, CancellationToken cancellationToken = default);
}
