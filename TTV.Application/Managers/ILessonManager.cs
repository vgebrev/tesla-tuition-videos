using TTV.Domain;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Application.Managers;

public interface ILessonManager
{
    Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<Page<Lesson>> GetLessonsOwnedByUserAsync(PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Page<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
}