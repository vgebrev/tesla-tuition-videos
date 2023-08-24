using Microsoft.AspNetCore.Mvc;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.Videos;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly ILogger<VideoController> logger;
    private readonly IVideoStreamLoader videoStreamLoader;
    private readonly IUserIdentityService userIdentityService;

    public VideoController(ILogger<VideoController> logger, IVideoStreamLoader videoStreamLoader, IUserIdentityService userIdentityService)
    {
        this.logger = logger;
        this.videoStreamLoader = videoStreamLoader;
        this.userIdentityService = userIdentityService;
    }

    // TODO: This should probably move to the lesson controller, and this controller can serve "always free" videos (eg shorts, "meet the teacher" etc) once that concept exists
    [HttpGet("{lessonId}")]
    public async Task<IResult> GetVideoStreamAsync([FromRoute] int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({LessonId})", nameof(GetVideoStreamAsync), lessonId);
        var stream = await videoStreamLoader.LoadLessonVideoStreamAsync(lessonId, userIdentityService.Email, cancellationToken);
        if (stream == Stream.Null)
        {
            return Results.NotFound();
        }

        return Results.File(stream, contentType: "video/mp4", enableRangeProcessing: true);
    }
}
