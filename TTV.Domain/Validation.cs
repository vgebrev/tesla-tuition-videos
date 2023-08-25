namespace TTV.Domain;

public class Validation
{
    private readonly Func<bool> failIf;
    
    public Validation(Func<bool> failIf, string error)
    {
        this.failIf = failIf;
        Error = error;
    }

    public bool IsFailed => failIf();
    public string Error { get; }
}

public static class ValidationExtensions
{
    public static Result GetResult(this Validation[] validations)
    {
        var validationError = validations.FirstOrDefault(x => x.IsFailed)?.Error;
        return new Result(string.IsNullOrEmpty(validationError), validationError);
    }
}

    
