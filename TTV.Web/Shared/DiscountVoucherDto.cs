namespace TTV.Web.Shared;

public record DiscountVoucherDto
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public decimal Balance { get; init; }
    public DateOnly? ExpirationDate { get; init; }
    public string? Note { get; init; }
    public UserDto? ClaimedBy { get; init; }
    public UserDto IssuedBy { get; init; } = new();
    public DateTime IssuedAt { get; init; }
}
