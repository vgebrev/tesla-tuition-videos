namespace TTV.Domain;

public class Validation(Func<bool> failIf, string error)
{
    private readonly Func<bool> failIf = failIf;

    public bool IsFailed => failIf();
    public string Error { get; } = error;
}

public static class ValidationExtensions
{
    public static Result GetResult(this Validation[] validations)
    {
        var validationError = validations.FirstOrDefault(x => x.IsFailed)?.Error;
        return new Result(string.IsNullOrEmpty(validationError), validationError);
    }
}

    
