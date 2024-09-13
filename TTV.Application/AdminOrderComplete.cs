using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application;
public class AdminOrderComplete : IAdminOrderComplete
{
    public async Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
