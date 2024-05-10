using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class OrderRepository(DataContext dataContext) : IOrderRepository
{
    private readonly DataContext dataContext = dataContext;

    public void Add(Order order)
    {
        dataContext.Orders.Add(order);
    }

    public async Task<IEnumerable<Order>> GetPlacedByUserListAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dataContext.Orders.TagWithCallSite()
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .Where(order => order.PlacedBy.Id == userId)
            .OrderByDescending(order => order.PlacedOn).ThenBy(order => order.Status)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Orders.TagWithCallSite()
            .Include(order => order.Lessons)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .SingleOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Orders.TagWithCallSite()
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .SingleOrDefaultAsync(order => order.Id == orderId && order.PlacedBy.Id == ownerId, cancellationToken);
    }
}
