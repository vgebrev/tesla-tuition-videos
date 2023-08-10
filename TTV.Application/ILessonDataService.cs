using TTV.Web.Shared;

namespace TTV.Application
{
    public interface ILessonDataService
    {
        Task<IEnumerable<LessonDto>> GetLessonsAsync();
    }
}