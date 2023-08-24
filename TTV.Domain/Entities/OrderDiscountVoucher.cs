namespace TTV.Domain.Entities;

public class OrderDiscountVoucher
{
    public int VoucherId { get; private set; }
    public DiscountVoucher Voucher { get; set; } = new();
    public int OrderId { get; private set; }
    public Order Order { get; set; } = new();
    public decimal Amount { get; set; }
    public DateTime UsedAt { get; set; } = DateTime.Now;
}
