using FakeItEasy;
using Shouldly;
using Microsoft.Extensions.Options;
using TTV.Application;
using TTV.Domain.Entities;
using TTV.Infrastructure;

namespace TTV.Tests;

public class DiscountVoucherCodeGeneratorTests
{
    [Fact]
    public void TestCodeLength()
    {
        var fakeOptions = A.Fake<IOptionsSnapshot<SystemSettings>>();
        A.CallTo(() => fakeOptions.Value).Returns(new SystemSettings { DiscountVoucherPepper = "super secret pepper value" });

        var voucher = new DiscountVoucher();
        var generator = new DiscountVoucherCodeGenerator(fakeOptions);

        generator.Generate(voucher).Length.ShouldBe(12);
    }
}