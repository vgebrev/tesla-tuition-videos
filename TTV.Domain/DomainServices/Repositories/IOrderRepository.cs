using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Domain.DomainServices.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Task<Page<Order>> GetPlacedByUserListAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Page<Order>> GetListAsync(OrderListFilter filter, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default);
}
