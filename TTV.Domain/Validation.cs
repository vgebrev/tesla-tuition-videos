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
