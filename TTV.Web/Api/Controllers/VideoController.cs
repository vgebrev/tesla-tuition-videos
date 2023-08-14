using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TTV.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VideoController(ILogger<VideoController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{videoId}")]
        [Authorize]
        public IResult GetVideoStream([FromRoute] int videoId)
        {
            logger.LogInformation("{Method}({VideoId})", nameof(GetVideoStream), videoId);

            // TODO: Link to lessons
            var filename = "TTV-Lesson-Placeholder.mp4";
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Videos", filename);
            var stream = System.IO.File.OpenRead(path);
            return Results.File(stream, contentType: "video/mp4", fileDownloadName: Path.GetFileName(path), enableRangeProcessing: true);
        }

        [HttpGet("{videoId}/intro")]
        public IResult GetVideoIntroStream([FromRoute] int videoId)
        {
            logger.LogInformation("{Method}({VideoId})", nameof(GetVideoIntroStream), videoId);

            // TODO: Link to lessons
            var filename = "TTV-Intro-Placeholder.mp4";
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Videos", filename);
            var stream = System.IO.File.OpenRead(path);
            return Results.File(stream, contentType: "video/mp4", fileDownloadName: Path.GetFileName(path), enableRangeProcessing: true);
        }
    }
}
