using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("tags")]
[ApiController]
public class TagsController(ILogger<TagsController> logger, ITagManager tagManager) : ControllerBase
{
    private readonly ILogger<TagsController> logger = logger;
    private readonly ITagManager tagManager = tagManager;

    [HttpGet]
    public async Task<IEnumerable<TagDto>> GetTags(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}()", nameof(GetTags));
        var tags = await tagManager.GetTagsAsync(cancellationToken);
        return tags.ToTagDtoEnumerable();
    }
}
