using Fluxor;
using Microsoft.AspNetCore.Components;
using TTV.Web.Blazor.Pages.Checkout.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.Checkout.Store;

public class Effects
{
    private readonly IOrderApiConsumer orderApi;
    private readonly IDiscountVoucherApiConsumer discountVoucherApi;
    private readonly NavigationManager navigationManager;

    public Effects(IOrderApiConsumer orderApi, IDiscountVoucherApiConsumer discountVoucherApi, NavigationManager navigationManager)
    {
        this.orderApi = orderApi;
        this.discountVoucherApi = discountVoucherApi;
        this.navigationManager = navigationManager;
    }

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

    [EffectMethod]
    public async Task HandleApplyVoucherRequest(ApplyVoucherRequest action, IDispatcher dispatcher)
    {
        try
        {
            var resultDto = await discountVoucherApi.ApplyDiscountVoucherAsync(action.VoucherCode, action.OrderId);
            dispatcher.Dispatch(new ApplyVoucherResponse() { Result = resultDto });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new ApplyVoucherError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
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

    [EffectMethod]
    public Task HandleCompleteOrderResponse(CompleteOrderResponse action, IDispatcher _)
    {
        if (action.Result.IsSuccess)
        {
            navigationManager.NavigateTo($"/order-complete/{action.Result.Value.Id}");
        }
        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleCancelOrderRequest(CancelOrderRequest action, IDispatcher dispatcher)
    {
        try
        {
            var resultDto = await orderApi.CancelOrderAsync(action.OrderId);
            dispatcher.Dispatch(new CancelOrderResponse() { Result = resultDto });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new CancelOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
