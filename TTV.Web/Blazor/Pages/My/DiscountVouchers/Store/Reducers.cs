using Fluxor;
using TTV.Web.Blazor.Pages.My.DiscountVouchers.Actions;

namespace TTV.Web.Blazor.Pages.My.DiscountVouchers.Store;

public static class Reducers
{
    [ReducerMethod(typeof(GetDiscountVouchersRequest))]
    public static MyDiscountVouchersState ReduceGetDiscountVouchersRequest(MyDiscountVouchersState state) =>
    state with
    {
        IsLoading = true
    };

    [ReducerMethod]
    public static MyDiscountVouchersState ReduceGetDiscountVouchersResponse(MyDiscountVouchersState state, GetDiscountVouchersResponse action) =>
        state with
        {
            IsLoading = false,
            DiscountVouchers = action.DiscountVouchers,
            Error = new()
        };

    [ReducerMethod]
    public static MyDiscountVouchersState ReduceGetDiscountVouchersError(MyDiscountVouchersState state, GetDiscountVouchersError action) =>
        state with
        {
            IsLoading = false,
            Error = new() { ErrorMessage = action.ErrorMessage, IsError = true }
        };
}
