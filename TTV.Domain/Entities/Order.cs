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

    public Result ApplyVoucher(DiscountVoucher voucher)
    {
        throw new NotImplementedException();
    }

    public User PlacedBy { get; set; } = new();
    public DateTime PlacedOn { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public string? StatusReason { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; }
    public virtual ICollection<OrderDiscountVoucher> AppliedVouchers { get; set; }

    public decimal TotalAmount => Lessons.Sum(lesson => lesson.PriceAt(PlacedOn).EffectivePrice) - AppliedVouchers.Sum(x => x.Amount);
}
