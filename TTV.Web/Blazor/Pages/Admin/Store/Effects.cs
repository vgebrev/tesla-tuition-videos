using Fluxor;
using TTV.Web.Blazor.Pages.Admin.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.Admin.Store;

public class Effects
{
    private readonly IDiscountVoucherApiConsumer discountVoucherApi;

    public Effects(IDiscountVoucherApiConsumer discountVoucherApi)
    {
        this.discountVoucherApi = discountVoucherApi;
    }

    [EffectMethod]
    public async Task HandleIssueDiscountVoucherRequest(IssueDiscountVoucherRequest action, IDispatcher dispatcher)
    {
        try
        {
            var dto = new DiscountVoucherIssueDto
            {
                Amount = action.Amount,
                ExpirationDate = action.ExpirationDate,
                Note = action.Note
            };
            var voucher = await discountVoucherApi.IssueAsync(dto);
            dispatcher.Dispatch(new IssueDiscountVoucherResponse() { DiscountVoucher = voucher });
            dispatcher.Dispatch(new GetDiscountVouchersRequest());
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new IssueDiscountVoucherError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod(typeof(GetDiscountVouchersRequest))]
    public async Task HandleGetDiscountVouchersRequest(IDispatcher dispatcher)
    {
        try
        {
            var vouchers = await discountVoucherApi.GetListAsync();
            dispatcher.Dispatch(new GetDiscountVouchersResponse() { DiscountVouchers = vouchers });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetDiscountVouchersError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
