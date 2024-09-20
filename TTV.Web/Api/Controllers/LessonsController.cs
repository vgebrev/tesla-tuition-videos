using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Domain.Filters;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("lessons")]
[ApiController]
public class LessonsController(ILogger<LessonsController> logger, ILessonManager lessonManager) : ControllerBase
{
    private readonly ILogger<LessonsController> logger = logger;
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
    public async Task<PageDto<LessonDto>> SearchLessons([FromBody] LessonSearchDto searchDto, [FromQuery] PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({@SearchDto},{@PageFilter})", nameof(SearchLessons), searchDto, pageFilter);
        if (pageFilter?.Take == null)
        {
            pageFilter = null;
        }
        var page = await lessonManager.SearchLessonsAsync(searchDto.SearchText, searchDto.SearchTagsIds, pageFilter, cancellationToken);
        return new PageDto<LessonDto>(page.Items.ToLessonDtoEnumerable(), page.PageInfo.ToPageInfoDto());
    }

    [HttpGet("own")]
    [Authorize]
    public async Task<PageDto<LessonDto>> GetLessonsOwnedByUser([FromQuery] PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({@PageFilter})", nameof(GetLessonsOwnedByUser), pageFilter);
        if (pageFilter?.Take == null)
        {
            pageFilter = null;
        }
        var page = await lessonManager.GetLessonsOwnedByUserAsync(pageFilter, cancellationToken);
        return new PageDto<LessonDto>(page.Items.ToLessonDtoEnumerable(), page.PageInfo.ToPageInfoDto());
    }
}
