namespace TTV.Web.Shared;
public record LearningPathDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public LookupDto[] Curricula { get; init; } = [];
    public LearningPathItemDto[] Items { get; init; } = [];
}
