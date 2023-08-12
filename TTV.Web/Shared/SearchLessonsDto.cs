namespace TTV.Web.Shared;

public record SearchLessonsDto
{
    public string? SearchText { get; init; }
    public int[]? SearchTagsIds { get; init; }
}
