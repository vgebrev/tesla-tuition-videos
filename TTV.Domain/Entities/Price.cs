namespace TTV.Domain.Entities;

public class Price : BaseEntity
{
    public Lesson Lesson { get; set; } = new();
    public decimal Amount { get; set; }
    public decimal? PromoAmount { get; set; }
    public DateOnly EffectiveDate { get; set; } = DateOnly.MinValue;

    public decimal EffectiveAmount => PromoAmount ?? Amount;
}
