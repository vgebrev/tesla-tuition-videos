namespace TTV.Application.Exceptions;
public class UserNotFoundException(Guid userId) : ApplicationException("User doesn't exist")
{
    public Guid UserId { get; } = userId;
}
