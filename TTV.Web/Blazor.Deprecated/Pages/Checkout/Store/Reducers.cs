using Fluxor;
using TTV.Web.Blazor.Pages.Checkout.Store.Actions;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Shared;

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
                ApplyVoucherResult = null,
                CancelOrderResult = null
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

        [ReducerMethod(typeof(ApplyVoucherRequest))]
        public static CheckoutState ReduceApplyVoucherRequest(CheckoutState state) =>
            state with
            {
                IsLoading = true,
                ApplyVoucherResult = null
            };

        [ReducerMethod]
        public static CheckoutState ReduceApplyVoucherResponse(CheckoutState state, ApplyVoucherResponse action)
        {
            OrderDto? order = state.Order;
            if (order != null && action.Result.IsSuccess)
            {
                order = action.Result.Value;
            }

            return state with
            {
                Order = order,
                IsLoading = false,
                ApplyVoucherResult = new(order?.AppliedDiscounts.LastOrDefault(), action.Result.IsSuccess, action.Result.Message),
                Error = new()
            };
        }

        [ReducerMethod]
        public static CheckoutState ReduceApplyVoucherError(CheckoutState state, ApplyVoucherError action) =>
            state with
            {
                ApplyVoucherResult = null,
                IsLoading = false,
                Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
            };

        [ReducerMethod(typeof(CancelOrderRequest))]
        public static CheckoutState ReduceCancelOrderRequest(CheckoutState state) =>
        state with
        {
            IsLoading = true,
        };

        [ReducerMethod]
        public static CheckoutState ReduceCancelOrderResponse(CheckoutState state, CancelOrderResponse action) =>
            state with
            {
                CancelOrderResult = action.Result,
                Order = action.Result.Value,
                IsLoading = false,
                Error = new()
            };

        [ReducerMethod]
        public static CheckoutState ReduceCancelOrderError(CheckoutState state, CancelOrderError action) =>
            state with
            {
                CancelOrderResult = null,
                IsLoading = false,
                Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
            };

        [ReducerMethod(typeof(InitiatePaymentRequest))]
        public static CheckoutState ReduceInitiatePaymentRequest(CheckoutState state) =>
            state with
            {
                IsLoading = true,
            };

        [ReducerMethod]
        public static CheckoutState ReduceInitiateOrderResponse(CheckoutState state, InitiatePaymentResponse action) =>
            state with
            {
                InitiatePaymentResult = action.Result,
                IsLoading = false,
                Error = new()
            };

        [ReducerMethod]
        public static CheckoutState ReduceInitiatePaymentError(CheckoutState state, InitiatePaymentError action) =>
            state with
            {
                InitiatePaymentResult = null,
                IsLoading = false,
                Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
            };
    }
}
