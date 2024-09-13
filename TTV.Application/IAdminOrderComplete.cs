using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application;
public interface IAdminOrderComplete
{
    Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default);
}
