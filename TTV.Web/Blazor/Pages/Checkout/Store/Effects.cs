using Fluxor;
using TTV.Web.Blazor.Pages.Checkout.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.Checkout.Store;

public class Effects
{
    private readonly IOrderApiConsumer apiConsumer;

    public Effects(IOrderApiConsumer apiConsumer)
    {
        this.apiConsumer = apiConsumer;
    }

    [EffectMethod]
    public async Task HandleGetOrderRequest(GetOrderRequest action, IDispatcher dispatcher)
    {
        try
        {
            var order = action.CurrentOrder;
            if (order == null || order.Id != action.OrderId)
            {
                order = await apiConsumer.GetOrderAsync(action.OrderId);
            }
            dispatcher.Dispatch(new GetOrderResponse() { Order = order });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
