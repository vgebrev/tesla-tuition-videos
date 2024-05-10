namespace TTV.Web.Shared;

public record OrderCreateDto
{
    public int[] LessonsIds { get; init; } = [];
}
