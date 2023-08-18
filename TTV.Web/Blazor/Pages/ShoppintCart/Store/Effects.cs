using Blazored.LocalStorage;
using Fluxor;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

public class Effects
{
    private readonly ILocalStorageService localStorageService;
    private const string LocalStorageKey = "TTV_ShoppingCartState_Lessons";

    public Effects(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task HandleLocalStorePersistRequest(LocalStorePersistRequest action, IDispatcher dispatcher)
    {
        try
        {
            await localStorageService.SetItemAsync(LocalStorageKey, action.Lessons);
            dispatcher.Dispatch(new LocalStorePersistResponse());
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new LocalStorePersistError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod(typeof(LocalStoreLoadRequest))]
    public async Task HandleLocalStoreLoadRequest(IDispatcher dispatcher)
    {
        try
        {
            var lessons = await localStorageService.GetItemAsync<LessonDto[]>(LocalStorageKey);
            dispatcher.Dispatch(new LocalStoreLoadResponse() { Lessons = lessons ?? Array.Empty<LessonDto>() });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new LocalStoreLoadError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
