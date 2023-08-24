using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public interface IOrderDataService
{
    Task<Order> CreateNewOrderAsync(int[] lessonsIds, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);
}