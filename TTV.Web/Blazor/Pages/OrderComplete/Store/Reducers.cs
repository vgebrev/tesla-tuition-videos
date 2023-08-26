using Fluxor;
using TTV.Web.Blazor.Pages.OrderComplete.Store.Actions;

namespace TTV.Web.Blazor.Pages.OrderComplete.Store;

public static class Reducers
{
    [ReducerMethod]
    public static OrderCompleteState ReduceConfirmOrderResponse(OrderCompleteState state, Checkout.Store.Actions.CompleteOrderResponse action) =>
          state with
          {
              Order = action.Result.Value,
              IsLoading = false,
              Error = action.Result.IsSuccess ? new() : new() { IsError = true, ErrorMessage = action.Result.Message ?? string.Empty }
          };


    [ReducerMethod(typeof(GetOrderRequest))]
    public static OrderCompleteState ReduceGetOrderRequest(OrderCompleteState state) =>
        state with
        {
            IsLoading = true,
        };

    [ReducerMethod]
    public static OrderCompleteState ReduceGetOrderResponse(OrderCompleteState state, GetOrderResponse action) =>

        state with
        {
            IsLoading = false,
            Order = action.Order,
            Error = new()
        };

    [ReducerMethod]
    public static OrderCompleteState ReduceGetOrderError(OrderCompleteState state, GetOrderError action) =>
        state with
        {
            IsLoading = false,
            Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
        };
}
