using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Task<IEnumerable<Order>> GetPlacedByUserListAsync(Guid userId, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default);
}
