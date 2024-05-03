using TTV.Web.Shared;
using static TTV.Web.Blazor.Pages.Checkout.PaymentDetail;

namespace TTV.Web.Blazor.Pages.Checkout.Store.Actions;

public record InitiatePaymentRequest
{
    public int OrderId { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}

public record InitiatePaymentResponse
{
    public ResultDto<PaymentDto?> Result { get; init; } = new(new(), false, null);
}

public record InitiatePaymentError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
