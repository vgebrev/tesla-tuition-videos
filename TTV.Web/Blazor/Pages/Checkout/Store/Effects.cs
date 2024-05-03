using Fluxor;
using Microsoft.AspNetCore.Components;
using TTV.Web.Blazor.Pages.Checkout.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Checkout.Store;

public class Effects
{
    private readonly IOrderApiConsumer orderApi;
    private readonly IDiscountVoucherApiConsumer discountVoucherApi;
    private readonly IPaymentApiConsumer paymentApi;

    public Effects(IOrderApiConsumer orderApi, IDiscountVoucherApiConsumer discountVoucherApi, IPaymentApiConsumer paymentApi, NavigationManager navigationManager)
    {
        this.orderApi = orderApi;
        this.discountVoucherApi = discountVoucherApi;
        this.paymentApi = paymentApi;
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

    [EffectMethod]
    public async Task HandleInitiatePaymentRequest(InitiatePaymentRequest action, IDispatcher dispatcher)
    {
        try
        {
            var dto = new PaymentInitiateDto()
            {
                OrderId = action.OrderId,
                PaymentMethod = action.PaymentMethod
            };
            var resultDto = await paymentApi.InitiateOrderPaymentAsync(dto);
            dispatcher.Dispatch(new InitiatePaymentResponse() { Result = resultDto });
        } catch(Exception){
            dispatcher.Dispatch(new InitiatePaymentError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
