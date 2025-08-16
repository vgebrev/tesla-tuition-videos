using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record ApplyVoucherRequest
{
    public int OrderId { get; init; }
    public string VoucherCode { get; init; } = string.Empty;
}

public record ApplyVoucherResponse
{
    public ResultDto<OrderDto?> Result { get; init; } = new(null, false, null);
}

public record ApplyVoucherError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
