namespace TTV.Web.Shared;
public record PaymentInitiateDto
{
    public int OrderId { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}
