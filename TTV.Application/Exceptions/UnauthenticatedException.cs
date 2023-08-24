namespace TTV.Application.Exceptions;
public class UnauthenticatedException : ApplicationException
{
    public UnauthenticatedException()
        : base("User is not authenticated")
    {   
    }
}
