using TTV.Domain;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Application.Managers;

public interface IDiscountVoucherManager
{
    Task<DiscountVoucher> IssueVoucherAsync(decimal amount, DateOnly? expirationDate = null, string? note = null, CancellationToken cancellationToken = default);
    Task<Page<DiscountVoucher>> GetListAsync(PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Page<DiscountVoucher>> GetClaimedByUserListAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default);
    Task<Result<OrderDiscountVoucher?>> ApplyDiscountVoucherAsync(string voucherCode, int orderId, CancellationToken cancellationToken = default);
}