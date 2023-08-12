namespace TTV.Web.Shared;

public record TagCategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Priority { get; init; }
}
