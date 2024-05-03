using Fluxor;
using Microsoft.AspNetCore.Components;
using TTV.Web.Blazor.Pages.OrderComplete.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.OrderComplete.Store;

public class Effects
{
    private readonly IOrderApiConsumer orderApi;

    public Effects(IOrderApiConsumer orderApi)
    {
        this.orderApi = orderApi;
    }

    [EffectMethod]
    public async Task HandleCompleteOrderRequest(CompleteOrderRequest action, IDispatcher dispatcher)
    {
        try
        {
            var resultDto = await orderApi.CompleteOrderAsync(action.OrderId);
            dispatcher.Dispatch(new CompleteOrderResponse() { Result = resultDto });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new CompleteOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    //[EffectMethod]
    //public Task HandleCompleteOrderResponse(CompleteOrderResponse action, IDispatcher _)
    //{
    //    if (action.Result.IsSuccess)
    //    {
    //        navigationManager.NavigateTo($"/order-complete/{action.Result.Value.Id}");
    //    }
    //    return Task.CompletedTask;
    //}

    [EffectMethod]
    public async Task HandleGetOrderRequest(GetOrderRequest action, IDispatcher dispatcher)
    {
        try
        {
            var order = action.CurrentOrder;
            if (order == null || order.Id != action.OrderId)
            {
                order = await orderApi.GetOrderAsync(action.OrderId);
            }
            dispatcher.Dispatch(new GetOrderResponse() { Order = order });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

}
