using System.Security.Claims;
using TTV.Domain.DomainServices;

namespace TTV.Web.Api;

public class ClaimsIdentityService : IUserIdentityService
{
    public ClaimsIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (Guid.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            UserId = userId;
        }
        Email = user?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public Guid? UserId { get; }

    public string? Email { get; }

    public bool IsAuthenticated => !string.IsNullOrEmpty(Email);

    public bool IsAnonymous => !IsAuthenticated;
}
