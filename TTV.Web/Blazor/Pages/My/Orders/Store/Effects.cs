using Fluxor;
using TTV.Web.Blazor.Pages.My.Orders.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.My.Orders.Store;

public class Effects(IOrderApiConsumer orderApi)
{
    private readonly IOrderApiConsumer orderApi = orderApi;

    [EffectMethod(typeof(GetOrdersRequest))]
    public async Task HandleGetDiscountVouchersRequest(IDispatcher dispatcher)
    {
        try
        {
            var orders = await orderApi.GetOwnListAsync();
            dispatcher.Dispatch(new GetOrdersResponse() { Orders = orders });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetOrdersError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
