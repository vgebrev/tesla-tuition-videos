using Microsoft.EntityFrameworkCore;
using TTV.Domain;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class OrderRepository(DataContext dataContext) : IOrderRepository
{
    private readonly DataContext dataContext = dataContext;

    public void Add(Order order)
    {
        dataContext.Orders.Add(order);
    }

    public async Task<Page<Order>> GetPlacedByUserListAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        var query = dataContext.Orders.AsNoTracking().TagWithCallSite()
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Videos.Where(video => video.VideoType == VideoType.FullLesson))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .Where(order => order.PlacedBy.Id == userId)
            .OrderByDescending(order => order.PlacedOn).ThenBy(order => order.Status);

        PageInfo? pageInfo = null;
        if (pageFilter != null)
        {
            var count = await query.CountAsync(cancellationToken);
            query = (IOrderedQueryable<Order>)query.Skip(pageFilter.Skip ?? 0).Take(pageFilter.Take ?? count);
            pageInfo = new PageInfo()
            {
                Skip = pageFilter.Skip ?? 0,
                Take = pageFilter.Take ?? count,
                Count = count
            };
        }

        return new Page<Order>()
        {
            PageInfo = pageInfo,
            Items = await query.ToListAsync(cancellationToken)
        };
    }

    public async Task<Page<Order>> GetListAsync(OrderListFilter filter, CancellationToken cancellationToken = default)
    {
        var query = dataContext.Orders.AsNoTracking().TagWithCallSite();

        if (filter.UserId.HasValue)
        {
            query = query.Where(order => order.PlacedBy.Id == filter.UserId);
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(order => order.Status == filter.Status);
        }

        if (filter.From.HasValue)
        {
            query = query.Where(order => order.PlacedOn >= filter.From);
        }

        if (filter.To.HasValue)
        {
            var to = filter.To.Value.AddDays(1).Date;
            query = query.Where(order => order.PlacedOn < to);
        }

        if (filter.IsFinalised)
        {
            query = query.Where(order => order.Status == OrderStatus.Cancelled || order.Status == OrderStatus.Completed);
        }
        else
        {
            query = query.Where(order => order.Status != OrderStatus.Cancelled && order.Status != OrderStatus.Completed);
        }

        query = query
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .OrderByDescending(order => order.PlacedOn).ThenBy(order => order.Status);

        var count = await query.CountAsync(cancellationToken);
        var pageInfo = new PageInfo()
        {
            Skip = filter.Skip ?? 0,
            Take = filter.Take ?? count,
            Count = count
        };

        return new Page<Order>()
        {
            PageInfo = pageInfo,
            Items = await query.Skip(filter.Skip ?? 0).Take(filter.Take ?? count).ToListAsync(cancellationToken)
        };
    }

    public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var placedById = await dataContext.Orders
            .Include(o => o.PlacedBy)
            .Where(o => o.Id == orderId)
            .Select(o => o.PlacedBy.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return await GetByIdAsync(orderId, placedById, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int orderId, Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Orders.TagWithCallSite()
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Videos.Where(video => video.VideoType == VideoType.FullLesson))
            .Include(order => order.Lessons)
                .ThenInclude(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(order => order.AppliedVouchers.OrderBy(map => map.UsedAt)).ThenInclude(map => map.Voucher).ThenInclude(voucher => voucher.OrdersAppliedTo)
            .Include(order => order.Payments)
            .Include(order => order.PlacedBy)
            .SingleOrDefaultAsync(order => order.Id == orderId && order.PlacedBy.Id == ownerId, cancellationToken);
    }
}
