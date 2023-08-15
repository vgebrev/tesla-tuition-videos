using Microsoft.AspNetCore.Mvc;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.Video;

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

    [HttpGet("{videoId}")]
    public async Task<IResult> GetVideoStreamAsync([FromRoute] int videoId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({VideoId})", nameof(GetVideoStreamAsync), videoId);
        var videoInfo = await videoStreamLoader.LoadAsync(videoId, userIdentityService.Email, cancellationToken);
        return Results.File(videoInfo.Stream, contentType: "video/mp4", fileDownloadName: videoInfo.Filename, enableRangeProcessing: true);
    }
}
