using Fluxor;
using TTV.Web.Blazor.Pages.Admin.Store.Actions;

namespace TTV.Web.Blazor.Pages.Admin.Store
{
    public static class Reducers
    {
        [ReducerMethod(typeof(IssueDiscountVoucherRequest))]
        public static AdminState ReduceIssueDiscountVoucherRequest(AdminState state) =>
            state with
            {
                IsLoading = true,
                IssuedVoucher = null
            };

        [ReducerMethod]
        public static AdminState ReduceIssueDiscountVoucherResponse(AdminState state, IssueDiscountVoucherResponse action) =>
            state with
            {
                IsLoading = false,
                IssuedVoucher = action.DiscountVoucher,
                Error = new()
            };

        [ReducerMethod]
        public static AdminState ReduceIssueDiscountVoucherError(AdminState state, IssueDiscountVoucherError action) =>
            state with
            {
                IsLoading = false,
                Error = new() { ErrorMessage = action.ErrorMessage, IsError = true }
            };

        [ReducerMethod(typeof(GetDiscountVouchersRequest))]
        public static AdminState ReduceGetDiscountVouchersRequest(AdminState state) =>
            state with
            {
                IsLoading = true
            };

        [ReducerMethod]
        public static AdminState ReduceGetDiscountVouchersResponse(AdminState state, GetDiscountVouchersResponse action) =>
            state with
            {
                IsLoading = false,
                DiscountVouchers = action.DiscountVouchers,
                Error = new()
            };

        [ReducerMethod]
        public static AdminState ReduceGetDiscountVouchersError(AdminState state, GetDiscountVouchersError action) =>
            state with
            {
                IsLoading = false,
                Error = new() { ErrorMessage = action.ErrorMessage, IsError = true }
            };
    }
}
