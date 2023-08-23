using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class DiscountVoucherMappings
{
    public static DiscountVoucherDto ToDiscountVoucherDto(this DiscountVoucher voucher) =>
        new()
        {
            Id = voucher.Id,
            Code = voucher.Code,
            Amount = voucher.Amount,
            Balance = voucher.RemainingAmount,
            ClaimedBy = voucher.ClaimedBy.ToUserDto(),
            ExpirationDate = voucher.ExpirationDate,
            Note = voucher.Note,
            IssuedBy = voucher.IssuedBy.ToUserDto()!,
            IssuedAt = voucher.IssuedAt
        };

    public static IEnumerable<DiscountVoucherDto> ToEnumerableDiscountVoucherDto(this IEnumerable<DiscountVoucher> vouchers) =>
        vouchers.Select(ToDiscountVoucherDto);
}
