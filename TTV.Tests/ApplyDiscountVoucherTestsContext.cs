using TTV.Domain.Entities;

namespace TTV.Tests;

public class ApplyDiscountVoucherTestsContext
{
    public ApplyDiscountVoucherTestsContext()
    {
        Init();
    }

    public void Init()
    {
        User = new User() { Id = Guid.NewGuid() };

        Lesson = new Lesson();
        Lesson.Prices.Add(new Price() { Amount = 100, Lesson = Lesson, EffectiveDate = DateOnly.MinValue });

        Order = new Order()
        {
            Status = OrderStatus.New,
            PlacedBy = User,
        };
        Order.Lessons.Add(Lesson);

        Voucher = new DiscountVoucher()
        {
            Amount = 100
        };
    }

    public User User { get; private set; } = default!;
    public Lesson Lesson { get; private set; } = default!;
    public Order Order { get; private set; } = default!;
    public DiscountVoucher Voucher { get; private set; } = default!;
}
