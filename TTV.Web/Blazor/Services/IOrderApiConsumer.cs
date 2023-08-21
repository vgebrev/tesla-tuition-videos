using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface IOrderApiConsumer
{
    Task<OrderDto> CreateNewOrderAsync(OrderCreateDto dto);
}
