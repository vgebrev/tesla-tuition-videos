namespace TTV.Application.Exceptions;
public class UserDoesntExistException : ApplicationException
{
    public UserDoesntExistException(Guid userId)
        : base("User doesn't exist")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
