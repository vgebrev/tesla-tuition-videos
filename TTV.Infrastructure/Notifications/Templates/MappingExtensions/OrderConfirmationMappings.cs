using TTV.Domain.Entities;

namespace TTV.Infrastructure.Notifications.Templates.MappingExtensions;
internal static class OrderConfirmationMappings
{
    internal static OrderConfirmationTemplate ToOrderConfirmationTemplateData(this Order order)
        => new(order.Id, "Customer", decimal.Round(order.OrderTotal, 2), order.Lessons.Select(lesson => lesson.ToTemplateData(order.PlacedOn)).ToArray());

    internal static OrderItem ToTemplateData(this Lesson lesson, DateTime orderDate)
        => new(lesson.Id, lesson.Title, decimal.Round(lesson.PriceAt(orderDate).EffectiveAmount, 2));
}
