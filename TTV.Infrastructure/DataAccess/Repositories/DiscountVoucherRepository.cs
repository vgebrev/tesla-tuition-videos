using Microsoft.EntityFrameworkCore;
using TTV.Domain;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class DiscountVoucherRepository(DataContext dataContext) : IDiscountVoucherRepository
{
    private readonly DataContext dataContext = dataContext;

    public void Add(DiscountVoucher voucher)
    {
        dataContext.DiscountVouchers.Add(voucher);
    }

    public async Task<Page<DiscountVoucher>> GetListAsync(Guid? userId = null, PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        var query = dataContext.DiscountVouchers.AsNoTracking().TagWithCallSite();

        if (userId is not null)
        {
            query = query.Where(voucher => voucher.ClaimedBy != null && voucher.ClaimedBy.Id == userId);
        }

        query = query.Include(voucher => voucher.ClaimedBy)
            .Include(voucher => voucher.IssuedBy)
            .Include(voucher => voucher.OrdersAppliedTo)
            .OrderByDescending(voucher => voucher.IssuedAt);

        PageInfo? pageInfo = null;
        if (pageFilter is not null)
        {
            var count = await query.CountAsync(cancellationToken);
            pageInfo = new PageInfo()
            {
                Skip = pageFilter?.Skip ?? 0,
                Take = pageFilter?.Take ?? count,
                Count = count
            };
            query = query.Skip(pageInfo.Skip).Take(pageInfo.Take);
        }
        return new Page<DiscountVoucher>() { 
            Items = await query.ToListAsync(cancellationToken), 
            PageInfo = pageInfo 
        };
    }

    public async Task<DiscountVoucher?> GetByCodeAsync(string voucherCode, CancellationToken cancellationToken = default)
    {
        return await dataContext.DiscountVouchers.TagWithCallSite()
            .Include(voucher => voucher.ClaimedBy)
            .Include(voucher => voucher.IssuedBy)
            .Include(voucher => voucher.OrdersAppliedTo)
            .OrderByDescending(voucher => voucher.IssuedAt)
            .SingleOrDefaultAsync(voucher => voucher.Code == voucherCode, cancellationToken);
    }
}
