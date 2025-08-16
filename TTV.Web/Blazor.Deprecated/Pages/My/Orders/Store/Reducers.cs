using Fluxor;
using TTV.Web.Blazor.Pages.My.Orders.Store.Actions;

namespace TTV.Web.Blazor.Pages.My.Orders.Store;

public class Reducers
{
    [ReducerMethod(typeof(GetOrdersRequest))]
    public static MyOrdersState ReduceGetDiscountVouchersRequest(MyOrdersState state) =>
        state with
        {
            IsLoading = true
        };

    [ReducerMethod]
    public static MyOrdersState ReduceGetDiscountVouchersResponse(MyOrdersState state, GetOrdersResponse action) =>
        state with
        {
            IsLoading = false,
            Orders = action.Orders,
            Error = new()
        };

    [ReducerMethod]
    public static MyOrdersState ReduceGetDiscountVouchersError(MyOrdersState state, GetOrdersError action) =>
        state with
        {
            IsLoading = false,
            Error = new() { ErrorMessage = action.ErrorMessage, IsError = true }
        };
}
