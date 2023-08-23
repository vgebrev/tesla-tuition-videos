using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class DiscountVoucherRepository : IDiscountVoucherRepository
{
    private readonly DataContext dataContext;

    public DiscountVoucherRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void Add(DiscountVoucher voucher)
    {
        dataContext.DiscountVouchers.Add(voucher);
    }

    public async Task<IEnumerable<DiscountVoucher>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await dataContext.DiscountVouchers.TagWithCallSite()
            .Include(voucher => voucher.ClaimedBy)
            .Include(voucher => voucher.IssuedBy)
            .Include(voucher => voucher.OrdersAppliedTo)
            .OrderByDescending(voucher => voucher.IssuedAt)
            .ToListAsync(cancellationToken);
    }
}
