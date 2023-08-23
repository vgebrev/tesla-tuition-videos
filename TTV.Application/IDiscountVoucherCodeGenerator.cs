using TTV.Domain.Entities;

namespace TTV.Application;

public interface IDiscountVoucherCodeGenerator
{
    string Generate(DiscountVoucher voucher);
}
