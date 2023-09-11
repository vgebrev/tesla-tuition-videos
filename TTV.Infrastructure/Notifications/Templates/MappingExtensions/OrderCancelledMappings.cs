using TTV.Domain.Entities;

namespace TTV.Infrastructure.Notifications.Templates.MappingExtensions;
internal static class OrderCancelledMappings
{
    internal static OrderCancelledTemplate ToOrderCancelledTemplateData(this Order order) =>
        new(order.Id, "Customer");
}
