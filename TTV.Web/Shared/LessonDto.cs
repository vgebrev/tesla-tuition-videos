namespace TTV.Web.Shared;

public record LessonDto
{
    public int Id { get; init; } 
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public LookupDto LessonType { get; init; } = default!;
    public SimpleTagDto[] Tags { get; init; } = default!;
    public int VideoId { get; init; }
}
