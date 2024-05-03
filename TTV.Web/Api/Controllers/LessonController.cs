using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILogger<LessonController> logger;
    private readonly ILessonManager lessonManager;

    public LessonController(ILogger<LessonController> logger, ILessonManager lessonManager)
    {
        this.logger = logger;
        this.lessonManager = lessonManager;
    }

    [HttpGet("{lessonId}")]
    public async Task<ActionResult<LessonDto>> GetLessonAsync([FromRoute]int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetLessonAsync), lessonId);
        var lesson = await lessonManager.GetLessonAsync(lessonId, cancellationToken);
        if (lesson == null)
        {
            return NotFound();
        }
        return Ok(lesson.ToLessonDto());
    }

    [HttpPost("search")]
    public async Task<IEnumerable<LessonDto>> SearchLessonsAsync([FromBody] LessonSearchDto searchDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({SearchDto})", nameof(SearchLessonsAsync), searchDto);
        var lessons = await lessonManager.SearchLessonsAsync(searchDto.SearchText, searchDto.SearchTagsIds, cancellationToken);
        return lessons.ToLessonDtoEnumerable();
    }

    [HttpGet("own")]
    [Authorize]
    public async Task<IEnumerable<LessonDto>> GetLessonsOwnedByUser(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}", nameof(GetLessonsOwnedByUser));
        var lessons = await lessonManager.GetLessonsOwnedByUserAsync(cancellationToken);
        return lessons.ToLessonDtoEnumerable();
    }
}
