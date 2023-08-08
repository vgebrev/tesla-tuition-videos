using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CourseController : ControllerBase
{
    private readonly ICourseDataService dataService;

    public CourseController(ICourseDataService dataService)
    {
        this.dataService = dataService;
    }

    [HttpGet]
    public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
    {
        return await dataService.GetCoursesAsync();
    }
}
