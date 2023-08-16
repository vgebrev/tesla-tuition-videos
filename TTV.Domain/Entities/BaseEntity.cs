namespace TTV.Domain.Entities;

public class BaseEntity : BaseEntity<int>
{
}

public class BaseEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    public TKey Id { get; set; }
}
