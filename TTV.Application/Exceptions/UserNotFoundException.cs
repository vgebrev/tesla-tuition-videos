namespace TTV.Application.Exceptions;
public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(Guid userId)
        : base("User doesn't exist")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
