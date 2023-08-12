namespace TTV.Web.Shared;

public record LookupDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
