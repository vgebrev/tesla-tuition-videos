using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface ILessonApiConsumer
{
    Task<LessonDto> GetLessonAsync(int lessonId);
    Task<LessonDto[]> GetLessonsOwnedByCurrentUserAsync();
    Task<LessonDto[]> SearchLessonsAsync(LessonSearchDto dto);
}