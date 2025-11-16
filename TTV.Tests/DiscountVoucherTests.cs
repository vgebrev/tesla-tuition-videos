using Shouldly;
using TTV.Domain.Entities;

namespace TTV.Tests;

public class DiscountVoucherTests
{
    [Fact]
    public void DiscountVoucherClaimed()
    {
        var discountVoucher = new DiscountVoucher();
        var user = new User() { Id = Guid.NewGuid() };

        var result = discountVoucher.ClaimBy(user);

        result.IsSuccess.ShouldBeTrue();
        discountVoucher.ClaimedBy.ShouldBe(user);
    }

    [Fact]
    public void DiscountVoucherAlreadyClaimed()
    {
        var discountVoucher = new DiscountVoucher();
        var user1 = new User() { Id = Guid.NewGuid() };
        discountVoucher.ClaimBy(user1);

        var user2 = new User() { Id = Guid.NewGuid() };
        var result = discountVoucher.ClaimBy(user2);

        result.IsSuccess.ShouldBeFalse();
        result.Message.ShouldBe("The voucher has been used by someone else");
        discountVoucher.ClaimedBy.ShouldBe(user1);
    }

    [Fact]
    public void DiscountVoucherClaimedBySameUser()
    {
        var discountVoucher = new DiscountVoucher();
        var user = new User() { Id = Guid.NewGuid() };
        discountVoucher.ClaimBy(user);

        var result = discountVoucher.ClaimBy(user);

        result.IsSuccess.ShouldBeTrue();
        discountVoucher.ClaimedBy.ShouldBe(user);
    }
}
