namespace TTV.Web.Shared;

public record AppliedDiscountDto
{
    public decimal Amount { get; init; }
    public decimal VoucherBalance { get; init; }
    public string VoucherCode { get; init; } = string.Empty;
    public DateTime UsedAt { get; init; }
}
