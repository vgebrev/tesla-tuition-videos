namespace TTV.Domain;
public record Page<TEntity>
{
    public PageInfo? PageInfo { get; init; }
    public IEnumerable<TEntity> Items { get; init; } = [];
}
