using TTV.Web.Shared;

namespace TTV.Application
{
    public interface ICourseDataService
    {
        Task<IEnumerable<CourseDto>> GetCoursesAsync();
    }
}