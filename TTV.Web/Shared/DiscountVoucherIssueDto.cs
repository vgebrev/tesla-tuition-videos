namespace TTV.Web.Shared;

public record DiscountVoucherIssueDto
{
    public decimal Amount { get; init; }
    public DateOnly? ExpirationDate { get; init; }
    public string? Note { get; init; }
}
