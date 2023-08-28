using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public interface IOrderManager
{
    Task<Order> CreateNewOrderAsync(int[] lessonsIds, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<Result<Order>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default);
}