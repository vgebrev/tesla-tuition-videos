using Microsoft.AspNetCore.Mvc;
using TTV.Application;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagDataService dataService;

        public TagController(ITagDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<IEnumerable<TagDto>> GetTagsAsync()
        {
            return await dataService.GetTagsAsync();
        }
    }
}
