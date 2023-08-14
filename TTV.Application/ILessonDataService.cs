using TTV.Web.Shared;

namespace TTV.Application;

public interface ILessonDataService
{
    Task<IEnumerable<LessonDto>> GetLessonsAsync(CancellationToken cancellationToken = default);

    Task<LessonDto> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default);

    Task<IEnumerable<LessonDto>> SearchLessonsAsync(SearchLessonsDto searchDto, CancellationToken cancellationToken = default);
}