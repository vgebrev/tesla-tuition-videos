using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Domain.DomainServices.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Task<IEnumerable<Order>> GetPlacedByUserListAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetListAsync(OrderListFilter filter, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default);
}
