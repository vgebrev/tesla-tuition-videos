using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class OrderMappings
{
    public static OrderDto ToOrderDto(this Order order) =>
        new()
        {
            Id = order.Id,
            Lessons = order.Lessons.ToLessonDtoEnumerable().ToArray(),
            PlacedBy = order.PlacedBy.ToUserDto()!,
            PlacedOn = order.PlacedOn,
            Status = order.Status.ToLookupDto(),
            StatusReason = order.StatusReason,
            TotalAmount = order.TotalAmount,
            AppliedDiscounts = order.AppliedVouchers.Select(ToAppliedDiscountDto).ToArray()!
        };

    public static AppliedDiscountDto? ToAppliedDiscountDto(this OrderDiscountVoucher? orderDiscountVoucher)
    {
        if (orderDiscountVoucher == null)
        {
            return null;
        }

        return new()
        {
            Amount = orderDiscountVoucher.Amount,
            Balance = orderDiscountVoucher.Voucher.RemainingAmount,
            Code = orderDiscountVoucher.Voucher.Code
        };
    }
}
