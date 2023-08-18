using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface ILessonDataService
{
    Task<LessonDto> GetLessonAsync(int lessonId);
    Task<LessonDto[]> GetLessonsOwnedByCurrentUserAsync();
    Task<LessonDto[]> SearchLessonsAsync(SearchLessonsDto dto);
}