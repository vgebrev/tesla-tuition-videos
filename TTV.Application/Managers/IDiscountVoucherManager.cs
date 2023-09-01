using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public interface IDiscountVoucherManager
{
    Task<DiscountVoucher> IssueVoucherAsync(decimal amount, DateOnly? expirationDate = null, string? note = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<DiscountVoucher>> GetListAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DiscountVoucher>> GetClaimedByUserListAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<OrderDiscountVoucher?>> ApplyDiscountVoucherAsync(string voucherCode, int orderId, CancellationToken cancellationToken = default);
}