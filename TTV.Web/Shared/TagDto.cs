namespace TTV.Web.Shared;

public record TagDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public TagCategoryDto Category { get; init; } = new();
    public int LessonCount { get; init; }
}

public record SimpleTagDto
{
    public string Name { get; init; } = string.Empty;
    public int Priority { get; init; }
}
