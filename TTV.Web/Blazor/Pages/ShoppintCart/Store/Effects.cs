using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

public class Effects
{
    private readonly ILocalStorageService localStorageService;
    private readonly NavigationManager navigationManager;
    private const string LocalStorageKey = "TTV_ShoppingCartState_Lessons";

    public Effects(ILocalStorageService localStorageService, NavigationManager navigationManager)
    {
        this.localStorageService = localStorageService;
        this.navigationManager = navigationManager;
    }

    [EffectMethod(typeof(ClearCart))]
    public static Task HandleClearCart(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new LocalStorePersistRequest());
        return Task.CompletedTask;
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

    [EffectMethod]
    public async Task HandleConfirmOrderRequest(ConfirmOrderRequest action, IDispatcher dispatcher)
    {
        //TODO: ApiCall
        await Task.Delay(400);
        var order = await Task.FromResult(new OrderDto()
        {
            Id = 1,
            Lessons = action.Lessons,
        });
        dispatcher.Dispatch(new ConfirmOrderResponse() { Order = order });
    }

    [EffectMethod]
    public Task HandleConfirmOrdeResponse(ConfirmOrderResponse action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ClearCart());
        navigationManager.NavigateTo($"checkout/{action.Order.Id}");
        return Task.CompletedTask;
    }
}
