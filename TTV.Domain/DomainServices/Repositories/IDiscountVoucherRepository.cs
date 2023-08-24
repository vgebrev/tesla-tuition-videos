using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IDiscountVoucherRepository
{
    void Add(DiscountVoucher voucher);
    Task<DiscountVoucher?> GetByCodeAsync(string voucherCode, CancellationToken cancellationToken);
    Task<IEnumerable<DiscountVoucher>> GetListAsync(CancellationToken cancellationToken = default);
}
