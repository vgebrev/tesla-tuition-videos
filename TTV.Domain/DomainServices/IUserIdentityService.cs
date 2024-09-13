namespace TTV.Domain.DomainServices;

public interface IUserIdentityService
{
    public Guid? UserId { get; }
    public string? Email { get; }
    public bool IsAuthenticated { get; }
    public bool IsAnonymous { get; }
    public bool IsAdmin { get; }
}
