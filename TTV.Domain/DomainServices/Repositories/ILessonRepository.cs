using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface ILessonRepository
{
    Task<IEnumerable<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default);

    Task<Lesson?> GetByIdAsync(int lessonId, CancellationToken cancellationToken = default);
}
