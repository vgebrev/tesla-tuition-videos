using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext dataContext;

    public OrderRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void Add(Order order)
    {
        dataContext.Orders.Add(order);
    }

    public async Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Orders.TagWithCallSite()
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(order => order.AppliedVouchers).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.PlacedBy)
            .SingleOrDefaultAsync(order => order.Id == orderId && order.PlacedBy.Id == ownerId, cancellationToken);
    }
}
