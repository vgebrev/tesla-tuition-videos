using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ILogger<TagController> logger;
    private readonly ITagManager tagManager;

    public TagController(ILogger<TagController> logger, ITagManager tagManager)
    {
        this.logger = logger;
        this.tagManager = tagManager;
    }

    [HttpGet]
    public async Task<IEnumerable<TagDto>> GetTagsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}()", nameof(GetTagsAsync));
        var tags = await tagManager.GetTagsAsync(cancellationToken);
        return tags.ToTagDtoEnumerable();
    }
}
