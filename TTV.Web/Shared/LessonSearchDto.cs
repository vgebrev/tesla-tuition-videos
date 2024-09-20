namespace TTV.Web.Shared;

public record LessonSearchDto
{
    public string? SearchText { get; init; }
    public int[]? SearchTagsIds { get; init; }
    public int? Skip { get; init; }
    public int? Take { get; init; } = 12;
}
