using TTV.Domain;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Application.Managers;

public interface IOrderManager
{
    Task<Order> CreateNewOrderAsync(int[] lessonsIds, CancellationToken cancellationToken = default);
    Task<Page<Order>> GetPlacedByUserListAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Page<Order>> GetListAsync(OrderListFilter filter, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Result<Order>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default);
}