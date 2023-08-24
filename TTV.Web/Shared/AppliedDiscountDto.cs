namespace TTV.Web.Shared;

public record AppliedDiscountDto
{
    public decimal Amount { get; init; }
    public decimal Balance { get; init; }
    public string Code { get; init; } = string.Empty;
}
