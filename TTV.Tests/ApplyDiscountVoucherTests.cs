using FluentAssertions;
using TTV.Domain.Entities;

namespace TTV.Tests;
public  class ApplyDiscountVoucherTests(ApplyDiscountVoucherTestsContext context) : IClassFixture<ApplyDiscountVoucherTestsContext>
{
    private readonly ApplyDiscountVoucherTestsContext context = context;

    [Theory]
    [InlineData(100, 100)]
    [InlineData(60, 100)]
    [InlineData(100, 60)]
    public void DiscountVoucherApplied(int voucherAmount, int lessonPrice)
    {
        context.Init();
        var discountVoucher = context.Voucher;
        discountVoucher.Amount = voucherAmount;
        var order = context.Order;
        order.Lessons.Single().Prices.Single().Amount = lessonPrice;
        var expectedDiscountAmount = Math.Min(voucherAmount, order.TotalAmount);

        var result = order.ApplyDiscountVoucher(discountVoucher);

        result.IsSuccess.Should().BeTrue();
        discountVoucher.RemainingAmount.Should().Be(voucherAmount - expectedDiscountAmount);
        discountVoucher.ClaimedBy.Should().Be(order.PlacedBy);
        discountVoucher.OrdersAppliedTo.Single().Order.Should().Be(order);
        order.TotalAmount.Should().Be(lessonPrice - expectedDiscountAmount);
        order.AppliedVouchers.Single().Voucher.Should().Be(discountVoucher);
    }

    [Theory]
    [InlineData(OrderStatus.New, true)]
    [InlineData(OrderStatus.AwaitingPayment, true)]
    [InlineData(OrderStatus.Completed, false)]
    [InlineData(OrderStatus.Cancelled, false)]
    public void DiscountVoucher_Validation_OrderStatus(OrderStatus status, bool expectedIsSuccess)
    {
        context.Init();
        var order = context.Order;
        order.Status = status;

        var result = order.ApplyDiscountVoucher(context.Voucher);

        result.IsSuccess.Should().Be(expectedIsSuccess);
    }

    [Theory]
    [InlineData(100, true)]
    [InlineData(0, false)]
    public void DiscountVoucher_Validation_TotalAmount(decimal orderTotal, bool expectedIsSuccess)
    {
        context.Init();
        var order = context.Order;
        order.Lessons.Single().Prices.Single().Amount = orderTotal;

        var result = order.ApplyDiscountVoucher(context.Voucher);

        result.IsSuccess.Should().Be(expectedIsSuccess);
    }

    [Fact]
    public void DiscountVoucher_Validation_VoucherClaimedByAnother()
    {
        context.Init();
        var discountVoucher = context.Voucher;
        discountVoucher.ClaimBy(new User() { Id = Guid.NewGuid() });

        var result = context.Order.ApplyDiscountVoucher(discountVoucher);

        result.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(1, true)]
    public void DiscountVoucher_Validation_ExpiredVoucher(int daysToExpiry, bool expectedIsSuccess)
    {
        context.Init();
        var discountVoucher = context.Voucher;
        discountVoucher.ExpirationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(daysToExpiry));

        var result = context.Order.ApplyDiscountVoucher(discountVoucher);

        result.IsSuccess.Should().Be(expectedIsSuccess);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(100, true)]
    public void DiscountVoucher_Validation_NoVoucherBalance(decimal voucherAmount, bool expectedIsSuccess)
    {
        context.Init();
        var discountVoucher = context.Voucher;
        discountVoucher.Amount = voucherAmount;

        var result = context.Order.ApplyDiscountVoucher(discountVoucher);

        result.IsSuccess.Should().Be(expectedIsSuccess);
    }
}
