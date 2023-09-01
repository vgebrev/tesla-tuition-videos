using Fluxor;
using TTV.Web.Blazor.Pages.My.DiscountVouchers.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.My.DiscountVouchers.Store;

public class Effects
{
    private readonly IDiscountVoucherApiConsumer discountVoucherApi;

    public Effects(IDiscountVoucherApiConsumer discountVoucherApi)
    {
        this.discountVoucherApi = discountVoucherApi;
    }

    [EffectMethod(typeof(GetDiscountVouchersRequest))]
    public async Task HandleGetDiscountVouchersRequest(IDispatcher dispatcher)
    {
        try
        {
            var vouchers = await discountVoucherApi.GetListAsync(claimedByCurrentUser: true);
            dispatcher.Dispatch(new GetDiscountVouchersResponse() { DiscountVouchers = vouchers });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetDiscountVouchersError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}