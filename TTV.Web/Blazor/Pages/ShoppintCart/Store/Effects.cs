using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

public class Effects
{
    private readonly ILocalStorageService localStorageService;
    private readonly NavigationManager navigationManager;
    private readonly IOrderApiConsumer orderApi;
    private const string LocalStorageKey = "TTV_ShoppingCartState_Lessons";

    public Effects(ILocalStorageService localStorageService, NavigationManager navigationManager, IOrderApiConsumer orderApi)
    {
        this.localStorageService = localStorageService;
        this.navigationManager = navigationManager;
        this.orderApi = orderApi;
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
        try
        {
            var dto = new OrderCreateDto()
            {
                LessonsIds = action.Lessons.Select(lesson => lesson.Id).ToArray(),
            };
            var order = await orderApi.CreateNewOrderAsync(dto);
            dispatcher.Dispatch(new ConfirmOrderResponse() { Order = order });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new ConfirmOrderError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod]
    public Task HandleConfirmOrdeResponse(ConfirmOrderResponse action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ClearCart());
        navigationManager.NavigateTo($"checkout/{action.Order.Id}");
        return Task.CompletedTask;
    }
}
