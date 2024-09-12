namespace TTV.Web.Shared;
public record PaymentDto
{
    public Guid Id { get; init; }
    public DateTime CreatedOn { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public string? ExternalIdentifier { get; init; }
    public decimal Amount { get; init; }
    public LookupDto Status { get; init; } = new();
}
