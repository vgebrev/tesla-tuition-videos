using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class OrderMappings
{
    public static OrderDto ToOrderDto(this Order order, bool includeLessons = true) =>
        new()
        {
            Id = order.Id,
            Lessons = includeLessons ? order.Lessons.ToLessonDtoEnumerable(order.PlacedOn, order.PlacedBy.Id).ToArray() : [],
            PlacedBy = order.PlacedBy.ToUserDto()!,
            PlacedOn = order.PlacedOn,
            Status = order.Status.ToLookupDto(),
            StatusReason = order.StatusReason,
            OrderTotal = order.OrderTotal,
            PaymentsTotal = order.PaymentsTotal,
            TotalAmount = order.TotalAmount,
            AppliedDiscounts = order.AppliedVouchers.OrderByDescending(av => av.UsedAt).Select(ToAppliedDiscountDto).ToArray()!,
            Payments = order.Payments.OrderByDescending(p => p.CreatedOn).ToEnumerablePaymentDto().ToArray(),
            HasOwnedLessons = order.HasOwnedLessons,
            IsFinalised = order.IsFinalised,
            IsPayable = order.IsPayable,
            CanComplete = order.CanComplete,
        };

    public static IEnumerable<OrderDto> ToEnumerableOrderDto(this IEnumerable<Order> orders, bool includeLessons = true) =>
        orders.Select(order => order.ToOrderDto(includeLessons));

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
            VoucherCode = orderDiscountVoucher.Voucher.Code,
            UsedAt = orderDiscountVoucher.UsedAt
        };
    }
}
