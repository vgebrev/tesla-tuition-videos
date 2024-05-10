using TTV.Domain.DomainServices;

namespace TTV.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Lessons = [];
        AppliedVouchers = [];
        Notifications = [];
        Payments = [];
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
        var validations = new Validation[]
{
            new(
                failIf: () => IsFinalised,
                error: $"Vouchers cannot be applied to orders with a status of \"{Status.ToDisplayString()}\""),
            new(
                failIf: () => TotalAmount <= 0,
                error: "There is no outstanding amount on the order"),
            new(
                failIf: () => voucher.ClaimedBy is not null && voucher.ClaimedBy != PlacedBy,
                error: "The voucher has been used by someone else"),
            new(
                failIf: () => voucher.ExpirationDate.HasValue && voucher.ExpirationDate.Value < DateOnly.FromDateTime(DateTime.Today),
                error: "The voucher has expired"),
            new(
                failIf: () => voucher.RemainingAmount <= 0,
                error: "The voucher has no balance remaining")
        };
        var validationResult = validations.GetResult();
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

    public Result<Order> Complete()
    {
        var validations = new Validation[]
        {
            new(
                failIf: () => Status == OrderStatus.Completed,
                error: $"The order is already completed"),
            new(
                failIf: () => Status == OrderStatus.Cancelled,
                error: $"The order has been cancelled"),
            new(
                failIf: () => TotalAmount > 0,
                error: $"The order has an outstanding amount of R{TotalAmount:0}"),
            new(
                failIf:() => HasOwnedLessons,
            error: $"The order has lessons that have already been purchased")
        };

        var validationResult = validations.GetResult();
        if (!validationResult.IsSuccess)
        {
            return new Result<Order>(this, validationResult.IsSuccess, validationResult.Message);
        }
        
        foreach (var lesson in Lessons)
        {
            if (lesson.OwnedBy.Any(user => user == PlacedBy))
            {
                continue;
            }

            PlacedBy.OwnedLessons.Add(lesson);
            lesson.OwnedBy.Add(PlacedBy);
        }
        Status = OrderStatus.Completed;
        return new Result<Order>(this, true, null);
    }

    public Result<Order> Cancel(bool refundAppliedVouchers = true)
    {
        if (IsFinalised)
        {
            return new Result<Order>(this, false, $"The order is already {Status.ToDisplayString().ToLower()}");
        }

        if (refundAppliedVouchers)
        {
            foreach (var appliedVoucher in AppliedVouchers)
            {
                appliedVoucher.Voucher.OrdersAppliedTo.Remove(appliedVoucher);
            }
            AppliedVouchers.Clear();
        }

        Status = OrderStatus.Cancelled;
        return new Result<Order>(this, true, $"Order cancelled {(refundAppliedVouchers ? "and vouchers refunded" : "")}");
    }

    public void CancelPendingPayments(string reason)
    {
        var pendingPayments = Payments.Where(payment => payment.Status == PaymentStatus.Pending);
        foreach (var pendingPayment in pendingPayments)
        {
            pendingPayment.Cancel(reason);
        }
    }

    public User PlacedBy { get; set; } = new();
    public DateTime PlacedOn { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public string? StatusReason { get; set; }
    
    public virtual ICollection<Lesson> Lessons { get; private set; }
    public virtual ICollection<OrderDiscountVoucher> AppliedVouchers { get; private set; }
    public virtual ICollection<Notification> Notifications { get; private set; }
    public virtual ICollection<Payment> Payments { get; private set; }

    public decimal OrderTotal => Lessons.Sum(lesson => lesson.PriceAt(PlacedOn).EffectiveAmount);
    public decimal PaymentsTotal => AppliedVouchers.Sum(x => x.Amount) + Payments.Where(x => x.Status == PaymentStatus.Paid).Sum(x => x.Amount);
    public decimal TotalAmount => OrderTotal - PaymentsTotal;
    public bool HasOwnedLessons => Lessons.Any(lesson => lesson.OwnedBy.Any(user => user == PlacedBy));
    public bool IsFinalised => Status == OrderStatus.Cancelled || Status == OrderStatus.Completed;
    public bool IsPayable => !IsFinalised && !HasOwnedLessons && TotalAmount > 0;
    public bool CanComplete => !IsFinalised && !HasOwnedLessons && TotalAmount <= 0;
}
