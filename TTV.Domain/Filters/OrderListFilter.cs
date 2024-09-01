using TTV.Domain.Entities;

namespace TTV.Domain.Filters;
public record OrderListFilter
{
    public Guid? UserId { get; init; } = null;
    public OrderStatus? Status { get; init; } = null;
    public bool IsFinalised { get; init; } = false;
    public DateTime? From { get; init; } = null;
    public DateTime? To { get; init; } = null;
}
