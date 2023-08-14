using Microsoft.AspNetCore.Mvc;
using TTV.Application;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILogger<LessonController> logger;
    private readonly ILessonDataService dataService;

    public LessonController(ILogger<LessonController> logger, ILessonDataService dataService)
    {
        this.logger = logger;
        this.dataService = dataService;
    }

    [HttpGet]
    public async Task<IEnumerable<LessonDto>> GetLessonsAsync(CancellationToken cancellationToken)
    {
        return await dataService.GetLessonsAsync(cancellationToken);
    }

    [HttpPost("search")]
    public async Task<IEnumerable<LessonDto>> SearchLessonsAsync([FromBody] SearchLessonsDto searchDto, CancellationToken cancellationToken)
    {
        logger.LogInformation("{Method}({SearchDto})", nameof(SearchLessonsAsync), searchDto);
        return await dataService.SearchLessonsAsync(searchDto, cancellationToken);
    }
}
