using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IDiscountVoucherRepository
{
    void Add(DiscountVoucher voucher);

    Task<IEnumerable<DiscountVoucher>> GetListAsync(CancellationToken cancellationToken = default);
}
