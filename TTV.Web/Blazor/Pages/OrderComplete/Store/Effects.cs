using Fluxor;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.OrderComplete.Store;

public class Effects(IOrderApiConsumer orderApi)
{
    private readonly IOrderApiConsumer orderApi = orderApi;

    [EffectMethod]
    public async Task HandleCompleteOrderRequest(CompleteOrderRequest action, IDispatcher dispatcher)
    {
        try
        {
            var resultDto = await orderApi.CompleteOrderAsync(action.OrderId);
            dispatcher.Dispatch(new CompleteOrderResponse() { Result = resultDto });
            if (resultDto.IsSuccess)
            {
                dispatcher.Dispatch(new LessonsList.Store.Actions.UpdateLessons() { Lessons = resultDto.Value.Lessons });
                dispatcher.Dispatch(new My.Lessons.Store.Actions.AddLessons() { Lessons = resultDto.Value.Lessons });
            }
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new CompleteOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
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

}
