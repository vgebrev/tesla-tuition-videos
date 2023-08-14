using Microsoft.AspNetCore.Mvc;
using TTV.Application;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> logger;
        private readonly ITagDataService dataService;

        public TagController(ILogger<TagController> logger, ITagDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<IEnumerable<TagDto>> GetTagsAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("{Method}()", nameof(GetTagsAsync));
            return await dataService.GetTagsAsync(cancellationToken);
        }
    }
}
