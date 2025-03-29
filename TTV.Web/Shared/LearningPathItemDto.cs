namespace TTV.Web.Shared;
public record LearningPathItemDto
{
    public int Id { get; init; }
    public string Name    { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Sequence { get; init; }
    public LearningPathItemDto[] Items { get; init; } = [];
    public LookupDto[] Curricula { get; init; } = [];
    public int? LessonId { get; init; }
}
