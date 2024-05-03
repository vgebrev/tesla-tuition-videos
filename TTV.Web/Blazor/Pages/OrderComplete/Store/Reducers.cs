using Fluxor;

namespace TTV.Web.Blazor.Pages.OrderComplete.Store;

public static class Reducers
{

    [ReducerMethod(typeof(CompleteOrderRequest))]
    public static OrderCompleteState ReduceCompleteOrderRequest(OrderCompleteState state) =>
        state with
        {
            IsLoading = true,
        };

    [ReducerMethod]
    public static OrderCompleteState ReduceCompleteOrderResponse(OrderCompleteState state, CompleteOrderResponse action) =>
          state with
          {
              CompleteOrderResult = action.Result,
              Order = action.Result.Value,
              IsLoading = false,
              Error = action.Result.IsSuccess ? new() : new() { IsError = true, ErrorMessage = action.Result.Message ?? string.Empty }
          };

    [ReducerMethod]
    public static OrderCompleteState ReduceCompleteOrderError(OrderCompleteState state, CompleteOrderError action) =>
    state with
    {
        CompleteOrderResult = null,
        Order = null,
        IsLoading = false,
        Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
    };

    //[ReducerMethod(typeof(GetOrderRequest))]
    //public static OrderCompleteState ReduceGetOrderRequest(OrderCompleteState state) =>
    //    state with
    //    {
    //        IsLoading = true,
    //    };

    //[ReducerMethod]
    //public static OrderCompleteState ReduceGetOrderResponse(OrderCompleteState state, GetOrderResponse action) =>

    //    state with
    //    {
    //        IsLoading = false,
    //        Order = action.Order,
    //        Error = new()
    //    };

    //[ReducerMethod]
    //public static OrderCompleteState ReduceGetOrderError(OrderCompleteState state, GetOrderError action) =>
    //    state with
    //    {
    //        IsLoading = false,
    //        Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
    //    };
}
