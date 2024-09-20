using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Domain.DomainServices.Repositories;

public interface IDiscountVoucherRepository
{
    void Add(DiscountVoucher voucher);
    Task<DiscountVoucher?> GetByCodeAsync(string voucherCode, CancellationToken cancellationToken);
    Task<Page<DiscountVoucher>> GetListAsync(Guid? userId = null, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
}
