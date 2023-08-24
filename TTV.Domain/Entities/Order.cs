using TTV.Domain.DomainServices;

namespace TTV.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Lessons = new HashSet<Lesson>();
        AppliedVouchers = new HashSet<OrderDiscountVoucher>();
    }

    public void AddLessons(IEnumerable<Lesson> lessons)
    {
        foreach (var lesson in lessons)
        {
            Lessons.Add(lesson);
        }
    }

    public Result<OrderDiscountVoucher?> ApplyDiscountVoucher(DiscountVoucher voucher)
    {
        if (Status != OrderStatus.New)
        {
            return new Result<OrderDiscountVoucher?>(null, false, $"Vouchers cannot be applied to orders with a status of '{Status.ToDisplayString()}'");
        }

        if (TotalAmount <= 0)
        {
            return new Result<OrderDiscountVoucher?>(null, false, "The is no outstanding amount on the order");
        }

        if (PlacedBy != voucher.ClaimedBy)
        {
            return new Result<OrderDiscountVoucher?>(null, false, "The voucher has been used by someone else");
        }

        if (voucher.ExpirationDate.HasValue && voucher.ExpirationDate.Value < DateOnly.FromDateTime(DateTime.Today))
        {
            return new Result<OrderDiscountVoucher?>(null, false, "The voucher has expired");
        }

        if (voucher.RemainingAmount <= 0)
        {
            return new Result<OrderDiscountVoucher?>(null, false, "The voucher has no balance remaining");
        }

        var amount = Math.Min(voucher.RemainingAmount, TotalAmount);

        var apply = new OrderDiscountVoucher
        {
            Order = this,
            Voucher = voucher,
            Amount = amount
        };
        AppliedVouchers.Add(apply);
        voucher.OrdersAppliedTo.Add(apply);

        return new Result<OrderDiscountVoucher?>(apply, true, $"A discount of R{amount:0} has been applied. The voucher has a remaining balance of R{voucher.RemainingAmount:0}.");
    }

    public User PlacedBy { get; set; } = new();
    public DateTime PlacedOn { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public string? StatusReason { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; }
    public virtual ICollection<OrderDiscountVoucher> AppliedVouchers { get; set; }

    public decimal TotalAmount => Lessons.Sum(lesson => lesson.PriceAt(PlacedOn).EffectiveAmount) - AppliedVouchers.Sum(x => x.Amount);
}
