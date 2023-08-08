using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TTV.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public VideoController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IResult GetVideoStream()
        {
            var filename = "Ions and Valency.mp4";
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Videos", filename);
            var stream = System.IO.File.OpenRead(path);
            return Results.File(stream, contentType: "video/mp4", fileDownloadName: Path.GetFileName(path), enableRangeProcessing: true);
        }
    }
}
