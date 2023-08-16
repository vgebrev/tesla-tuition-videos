using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TTV.Application.DataServices;
using TTV.Web.Api.MappingExtensions;
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
        var lessons = await dataService.GetLessonsAsync(cancellationToken);
        return lessons.ToLessonDtoEnumerable();
    }

    [HttpGet("{lessonId}")]
    public async Task<ActionResult<LessonDto>> GetLessonAsync([FromRoute]int lessonId, CancellationToken cancellationToken)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetLessonAsync), lessonId);
        var lesson = await dataService.GetLessonAsync(lessonId, cancellationToken);
        if (lesson == null)
        {
            return NotFound();
        }
        return Ok(lesson.ToLessonDto());
    }
    [HttpPost("search")]
    public async Task<IEnumerable<LessonDto>> SearchLessonsAsync([FromBody] SearchLessonsDto searchDto, CancellationToken cancellationToken)
    {
        logger.LogInformation("{Method}({SearchDto})", nameof(SearchLessonsAsync), searchDto);
        var lessons = await dataService.SearchLessonsAsync(searchDto.SearchText, searchDto.SearchTagsIds, cancellationToken);
        return lessons.ToLessonDtoEnumerable();
    }
}
