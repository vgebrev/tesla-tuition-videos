using Microsoft.AspNetCore.Mvc;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.Videos;

namespace TTV.Web.Api.Controllers;

[Route("videos")]
[ApiController]
public class VideosController(ILogger<VideosController> logger, IVideoStreamLoader videoStreamLoader, IUserIdentityService userIdentityService) : ControllerBase
{
    // TODO: This should probably move to the lesson controller, and this controller can serve "always free" videos (eg shorts, "meet the teacher" etc) once that concept exists
    [HttpGet("lesson/{lessonId}")]
    public async Task<IResult> GetVideoStream([FromRoute] int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetVideoStream), lessonId);
        var stream = await videoStreamLoader.LoadLessonVideoStreamAsync(lessonId, userIdentityService.Email, cancellationToken);
        if (stream == Stream.Null)
        {
            return Results.NotFound();
        }

        return Results.File(stream, contentType: "video/mp4", enableRangeProcessing: true);
    }

    [HttpGet("lesson/{lessonId}/thumbnail")]
    [ResponseCache(Duration = 60 * 60 * 24 * 7)] // 1 week
    public async Task<IResult> GetLessonThumbnail([FromRoute] int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetLessonThumbnail), lessonId);
        var stream = await videoStreamLoader.LoadThumbnailStreamAsync(lessonId, userIdentityService.Email, cancellationToken);
        if (stream == Stream.Null)
        {
            return Results.NotFound();
        }

        return Results.File(stream, contentType: "image/jpeg");
    }
}
