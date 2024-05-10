using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class DiscountVoucherRepository(DataContext dataContext) : IDiscountVoucherRepository
{
    private readonly DataContext dataContext = dataContext;

    public void Add(DiscountVoucher voucher)
    {
        dataContext.DiscountVouchers.Add(voucher);
    }

    public async Task<IEnumerable<DiscountVoucher>> GetListAsync(Guid? userId = null, CancellationToken cancellationToken = default)
    {
        var query = dataContext.DiscountVouchers.TagWithCallSite();

        if (userId is not null)
        {
            query = query.Where(voucher => voucher.ClaimedBy != null && voucher.ClaimedBy.Id == userId);
        }

        return await query.Include(voucher => voucher.ClaimedBy)
            .Include(voucher => voucher.IssuedBy)
            .Include(voucher => voucher.OrdersAppliedTo)
            .OrderByDescending(voucher => voucher.IssuedAt)
            .ToListAsync(cancellationToken);
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
