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
        var validationResult = ValidateVoucher(voucher);
        if (!validationResult.IsSuccess)
        {
            return new Result<OrderDiscountVoucher?>(null, false, validationResult.Message);
        }

        voucher.ClaimBy(PlacedBy);
        var amount = Math.Min(voucher.RemainingAmount, TotalAmount);
        var apply = new OrderDiscountVoucher
        {
            Order = this,
            Voucher = voucher,
            Amount = amount,
            UsedAt = DateTime.Now
        };
        AppliedVouchers.Add(apply);
        voucher.OrdersAppliedTo.Add(apply);

        return new Result<OrderDiscountVoucher?>(apply, true, $"A discount of R{amount:0} has been applied. The voucher has a remaining balance of R{voucher.RemainingAmount:0}.");
    }

    private Result ValidateVoucher(DiscountVoucher voucher)
    {
        var validations = new Validation[]
        {
            new Validation(
                failIf: () => Status != OrderStatus.New,
                error: $"Vouchers cannot be applied to orders with a status of '{Status.ToDisplayString()}'"),
            new Validation(
                failIf: () => TotalAmount <= 0,
                error: "There is no outstanding amount on the order"),
            new Validation(
                failIf: () => voucher.ClaimedBy is not null && voucher.ClaimedBy != PlacedBy,
                error: "The voucher has been used by someone else"),
            new Validation(
                failIf: () => voucher.ExpirationDate.HasValue && voucher.ExpirationDate.Value < DateOnly.FromDateTime(DateTime.Today),
                error: "The voucher has expired"),
            new Validation(
                failIf: () => voucher.RemainingAmount <= 0,
                error: "The voucher has no balance remaining")
        };

        var validationError = validations.FirstOrDefault(x => x.IsFailed)?.Error;
        return new Result(string.IsNullOrEmpty(validationError), validationError);
    }

    public User PlacedBy { get; set; } = new();
    public DateTime PlacedOn { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public string? StatusReason { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; }
    public virtual ICollection<OrderDiscountVoucher> AppliedVouchers { get; set; }

    public decimal TotalAmount => Lessons.Sum(lesson => lesson.PriceAt(PlacedOn).EffectiveAmount) - AppliedVouchers.Sum(x => x.Amount);
}
