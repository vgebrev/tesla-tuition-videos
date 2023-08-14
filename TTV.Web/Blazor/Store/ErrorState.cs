namespace TTV.Web.Blazor.Store;

public record ErrorState
{
    public bool IsError { get; init; }
    public string ErrorMessage { get; set; } = string.Empty;
}
