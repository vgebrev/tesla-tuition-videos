using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public interface ILessonManager
{
    Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lesson>> GetLessonsOwnedByUserAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default);
}