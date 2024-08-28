using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Notifications.Templates.MappingExtensions;
internal static class OrderCompletedMappings
{
    internal static OrderCompletedTemplate ToOrderCompletedTemplateData(this Order order)
    => new(order.Id,
           "Customer",
           decimal.Round(order.OrderTotal, 2),
           decimal.Round(order.PaymentsTotal, 2),
           order.Lessons.Select(lesson => lesson.ToTemplateData(order.PlacedOn)).ToArray(),
           order.AppliedVouchers.Select(x => x.ToTemplateData()).Concat(order.Payments.Where(p => p.Status == PaymentStatus.Paid).Select(p => p.ToTemplateData())).ToArray());

    internal static OrderItem ToTemplateData(this OrderDiscountVoucher appliedDiscount)
        => new(appliedDiscount.VoucherId, $"Discount Voucher ({appliedDiscount.Voucher.Code})", decimal.Round(appliedDiscount.Amount, 2));

    internal static OrderItem ToTemplateData(this Payment payment)
        => new(payment.Id.GetHashCode(), $"{payment.Type.ToDisplayString()} Payment", decimal.Round(payment.Amount, 2));

    
}
