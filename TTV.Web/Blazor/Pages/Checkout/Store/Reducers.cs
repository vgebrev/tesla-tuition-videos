using Fluxor;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;

namespace TTV.Web.Blazor.Pages.Checkout.Store
{
    public static class Reducers
    {
        [ReducerMethod]
        public static CheckoutState ReduceConfirmOrderResponse(CheckoutState state, ConfirmOrderResponse action)
        {
            return state with
            {
                Order = action.Order,
                IsLoading = false
            };
        }
    }
}
