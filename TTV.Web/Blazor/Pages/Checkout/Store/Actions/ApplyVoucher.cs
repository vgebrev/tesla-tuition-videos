using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record ApplyVoucherRequest
{
    public string VoucherCode { get; init; } = string.Empty;
}

public record ApplyVoucherResponse
{
    public OrderDto? Order { get; init; }
}

public record ApplyVoucherError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
