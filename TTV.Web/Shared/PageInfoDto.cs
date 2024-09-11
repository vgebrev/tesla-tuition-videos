namespace TTV.Web.Shared;
public record PageInfoDto
{
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 12;
    public int Count { get; init; }
}
