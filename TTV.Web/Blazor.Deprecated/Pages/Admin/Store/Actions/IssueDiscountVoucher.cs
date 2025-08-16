using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Admin.Store.Actions;

public record IssueDiscountVoucherRequest
{
    public decimal Amount { get; init; }
    public DateOnly? ExpirationDate { get; init; }
    public string? Note { get; init; }
}

public record IssueDiscountVoucherResponse
{
    public DiscountVoucherDto DiscountVoucher { get; init; } = new();
}

public record IssueDiscountVoucherError
{
    public string ErrorMessage { get; init; } = string.Empty;
}
