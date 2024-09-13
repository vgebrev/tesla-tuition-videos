namespace TTV.Domain.Filters;
public record PageFilter
{
    public int? Skip { get; init; }
    public int? Take { get; init; }
}
