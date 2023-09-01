using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.My.DiscountVouchers.Actions;

public record GetDiscountVouchersRequest
{
}

public record GetDiscountVouchersResponse
{
    public DiscountVoucherDto[] DiscountVouchers { get; init; } = Array.Empty<DiscountVoucherDto>();
}

public record GetDiscountVouchersError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
