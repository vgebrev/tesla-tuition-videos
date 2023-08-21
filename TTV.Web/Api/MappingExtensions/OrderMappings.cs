using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class OrderMappings
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto()
        {
            Id = order.Id,
            Lessons = order.Lessons.ToLessonDtoEnumerable().ToArray(),
            PlacedBy = order.PlacedBy.ToUserDto()!,
            PlacedOn = order.PlacedOn,
            Status = order.Status.ToLookupDto(),
            StatusReason = order.StatusReason,
            TotalAmount = order.TotalAmount,
        };
    }
}
