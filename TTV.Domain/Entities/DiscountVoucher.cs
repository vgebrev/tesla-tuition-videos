namespace TTV.Domain.Entities;

public class DiscountVoucher : BaseEntity
{
    public DiscountVoucher()
    {
        OrdersAppliedTo = [];
    }

    public string Code { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public string? Note { get; set; }
    public User IssuedBy { get; set; } = new();
    public DateTime IssuedAt { get; set; } = DateTime.Now;
    public User? ClaimedBy { get; private set; }
    public virtual ICollection<OrderDiscountVoucher> OrdersAppliedTo { get; set; }

    public decimal RemainingAmount => Amount - OrdersAppliedTo.Sum(use => use.Amount);

    public Result ClaimBy(User user)
    {
        if (ClaimedBy is not null && ClaimedBy != user)
        {
            return new Result(false, "The voucher has been used by someone else");
        }
        ClaimedBy = user;
        return new Result(true);
    }
}
