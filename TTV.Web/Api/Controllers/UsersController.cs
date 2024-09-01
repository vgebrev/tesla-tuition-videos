using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;
namespace TTV.Web.Api.Controllers;

[Route("users")]
[ApiController]
public class UsersController(ILogger<UsersController> logger, IUserManager userManager) : ControllerBase
{
    private readonly ILogger<UsersController> logger = logger;
    private readonly IUserManager userManager = userManager;

    [Authorize(Policy = "Admin")]
    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}()", nameof(GetUsers));
        var users = await userManager.GetAllsync(cancellationToken);
        return users.ToEnumerableUserDto();
    }
}
