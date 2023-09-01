using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class OrderMappings
{
    public static OrderDto ToOrderDto(this Order order) =>
        new()
        {
            Id = order.Id,
            Lessons = order.Lessons.ToLessonDtoEnumerable(order.PlacedOn).ToArray(),
            PlacedBy = order.PlacedBy.ToUserDto()!,
            PlacedOn = order.PlacedOn,
            Status = order.Status.ToLookupDto(),
            StatusReason = order.StatusReason,
            TotalAmount = order.TotalAmount,
            AppliedDiscounts = order.AppliedVouchers.Select(ToAppliedDiscountDto).ToArray()!,
            HasOwnedLessons = order.HasOwnedLessons,
            IsFinalised = order.IsFinalised,
            IsPayable = order.IsPayable,
            CanComplete = order.CanComplete,
        };

    public static IEnumerable<OrderDto> ToEnumerableOrderDto(this IEnumerable<Order> orders) =>
        orders.Select(ToOrderDto);

    public static AppliedDiscountDto? ToAppliedDiscountDto(this OrderDiscountVoucher? orderDiscountVoucher)
    {
        if (orderDiscountVoucher == null)
        {
            return null;
        }

        return new()
        {
            Amount = orderDiscountVoucher.Amount,
            VoucherBalance = orderDiscountVoucher.Voucher.RemainingAmount,
            VoucherCode = orderDiscountVoucher.Voucher.Code
        };
    }
}
