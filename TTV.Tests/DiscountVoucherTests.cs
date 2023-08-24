using FluentAssertions;
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

        result.IsSuccess.Should().BeTrue();
        discountVoucher.ClaimedBy.Should().Be(user);
    }

    [Fact]
    public void DiscountVoucherAlreadyClaimed()
    {
        var discountVoucher = new DiscountVoucher();
        var user1 = new User() { Id = Guid.NewGuid() };
        discountVoucher.ClaimBy(user1);

        var user2 = new User() { Id = Guid.NewGuid() };
        var result = discountVoucher.ClaimBy(user2);

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("The voucher has been used by someone else");
        discountVoucher.ClaimedBy.Should().Be(user1);
    }

    [Fact]
    public void DiscountVoucherClaimedBySameUser()
    {
        var discountVoucher = new DiscountVoucher();
        var user = new User() { Id = Guid.NewGuid() };
        discountVoucher.ClaimBy(user);

        var result = discountVoucher.ClaimBy(user);

        result.IsSuccess.Should().BeTrue();
        discountVoucher.ClaimedBy.Should().Be(user);
    }
}
