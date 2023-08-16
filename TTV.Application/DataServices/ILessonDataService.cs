using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public interface ILessonDataService
{
        Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Lesson>> GetLessonsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default);
}