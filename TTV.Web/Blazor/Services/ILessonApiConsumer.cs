using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface ILessonApiConsumer
{
    Task<LessonDto?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<LessonDto[]> GetLessonsOwnedByCurrentUserAsync(CancellationToken cancellationToken = default);
    Task<LessonDto[]> SearchLessonsAsync(LessonSearchDto dto, CancellationToken cancellationToken = default);
}