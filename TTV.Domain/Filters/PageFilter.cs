namespace TTV.Domain.Filters;
internal record PageFilter
{
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 12;
    public int Count { get; init; }
}
