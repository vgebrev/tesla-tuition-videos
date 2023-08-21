using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default);
}
