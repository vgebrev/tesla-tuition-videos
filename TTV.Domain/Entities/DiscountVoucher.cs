namespace TTV.Domain.Entities;

public class DiscountVoucher : BaseEntity
{
    public DiscountVoucher()
    {
        OrdersAppliedTo = new HashSet<OrderDiscountVoucher>();
    }

    public string Code { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public string? Note { get; set; }
    public User IssuedBy { get; set; } = new();
    public DateTime IssuedAt { get; set; } = DateTime.Now;
    public User? ClaimedBy { get; set; }
    public virtual ICollection<OrderDiscountVoucher> OrdersAppliedTo { get; set; }

    public decimal RemainingAmount => Amount - OrdersAppliedTo.Sum(use => use.Amount);
}
