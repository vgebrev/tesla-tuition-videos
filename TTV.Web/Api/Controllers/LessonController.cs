using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController(ILogger<LessonController> logger, ILessonManager lessonManager) : ControllerBase
{
    private readonly ILogger<LessonController> logger = logger;
    private readonly ILessonManager lessonManager = lessonManager;

    [HttpGet("{lessonId}")]
    public async Task<ActionResult<LessonDto>> GetLesson([FromRoute]int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetLesson), lessonId);
        var lesson = await lessonManager.GetLessonAsync(lessonId, cancellationToken);
        if (lesson == null)
        {
            return NotFound();
        }
        return Ok(lesson.ToLessonDto());
    }

    [HttpPost("search")]
    public async Task<IEnumerable<LessonDto>> SearchLessons([FromBody] LessonSearchDto searchDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({SearchDto})", nameof(SearchLessons), searchDto);
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
