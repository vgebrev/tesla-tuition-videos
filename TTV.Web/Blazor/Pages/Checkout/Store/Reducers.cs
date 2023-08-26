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
            OrderDto? order = null;
            if (state.Order != null)
            {
                var appliedDiscounts = new List<AppliedDiscountDto>(state.Order.AppliedDiscounts);
                if (action.Result.IsSuccess && action.Result.Value != null)
                {
                    appliedDiscounts.Add(action.Result.Value);
                } 

                order = state.Order with
                {
                    AppliedDiscounts = appliedDiscounts.ToArray(),
                    TotalAmount = state.Order.Lessons.Sum(l => l.CurrentPrice.EffectiveAmount) - appliedDiscounts.Sum(d => d.Amount)
                };
            }

            return state with
            {
                Order = order,
                IsLoading = false,
                ApplyVoucherResult = action.Result,
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

        [ReducerMethod(typeof(CompleteOrderRequest))]
        public static CheckoutState ReduceCompleteOrderRequest(CheckoutState state) =>
            state with
            {
                IsLoading = true,
            };

        [ReducerMethod]
        public static CheckoutState ReduceCompleteOrderResponse(CheckoutState state, CompleteOrderResponse action) =>
            state with
            {
                CompleteOrderResult = action.Result,
                Order = action.Result.Value,
                IsLoading = false,
                Error = new()
            };

        [ReducerMethod]
        public static CheckoutState ReduceCompleteOrderError(CheckoutState state, CompleteOrderError action) =>
            state with
            {
                CompleteOrderResult = null,
                IsLoading = false,
                Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
            };
    }
}
