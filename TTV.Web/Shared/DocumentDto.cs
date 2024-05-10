namespace TTV.Web.Shared;
public record DocumentDto
{
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public LookupDto DocumentType { get; init; } = new();
}
