using Fluxor;
using TTV.Web.Blazor.Pages.Checkout.Store.Actions;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;

namespace TTV.Web.Blazor.Pages.Checkout.Store
{
    public static class Reducers
    {
        [ReducerMethod]
        public static CheckoutState ReduceConfirmOrderResponse(CheckoutState state, ConfirmOrderResponse action) =>
            state with
            {
                Order = action.Order,
                IsLoading = false
            };


        [ReducerMethod(typeof(GetOrderRequest))]
        public static CheckoutState ReduceGetOrderRequest(CheckoutState state) =>
            state with
            {
                IsLoading = true,
            };

        [ReducerMethod]
        public static CheckoutState ReduceGetOrderResponse(CheckoutState state, GetOrderResponse action) =>

            state with
            {
                IsLoading = false,
                Order = action.Order,
                Error = new()
            };

        [ReducerMethod]
        public static CheckoutState ReduceGetOrderError(CheckoutState state, GetOrderError action) =>
            state with
            {
                IsLoading = false,
                Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
            };
    }
}
