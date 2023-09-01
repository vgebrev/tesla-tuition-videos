using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface IDiscountVoucherApiConsumer
{
    Task<DiscountVoucherDto> IssueAsync(DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default);
    Task<DiscountVoucherDto[]> GetListAsync(bool claimedByCurrentUser = false, CancellationToken cancellationToken = default);

    Task<ResultDto<AppliedDiscountDto?>> ApplyDiscountVoucherAsync(string voucherCode, int orderId, CancellationToken cancellationToken = default);
}