namespace TTV.Web.Shared;

public record PriceDto
{
    public decimal Amount { get; init; }
    public decimal? PromoAmount { get; init; }
}
