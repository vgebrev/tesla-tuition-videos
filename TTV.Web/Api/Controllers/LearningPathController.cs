using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;
[Route("learning-paths")]
[ApiController]
public class LearningPathController(ILogger<LearningPathController> logger, ILearningPathManager learningPathManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LearningPathDto>>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("{Method}()", nameof(GetAll));
        var learningPaths = await learningPathManager.GetallAsync(cancellationToken);
        return Ok(learningPaths.Select(learningPath => learningPath.ToLearningPathDto()));
    }
}
