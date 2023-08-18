using Fluxor;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ShoppingCartState ReduceAddLessonToCart(ShoppingCartState state, AddLesson action) =>
        state with
        {
            Lessons = new List<LessonDto>(state.Lessons.Union(new[] { action.Lesson })).ToArray()
        };

    [ReducerMethod]
    public static ShoppingCartState ReduceRemoveLessonFromCart(ShoppingCartState state, RemoveLesson action) =>
        state with
        {
            Lessons = new List<LessonDto>(state.Lessons.Where(lesson => lesson.Id != action.Lesson.Id)).ToArray()
        };

    [ReducerMethod(typeof(LocalStorePersistResponse))]
    public static ShoppingCartState ReduceLocalStorePersistResponse(ShoppingCartState state) =>
        state with
        {
            Error = new()
        };

    [ReducerMethod]
    public static ShoppingCartState ReduceLocalStorePersistError(ShoppingCartState state, LocalStorePersistError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage }
        };

    [ReducerMethod]
    public static ShoppingCartState ReduceLocalStoreLoadResponse(ShoppingCartState state, LocalStoreLoadResponse action) =>
        state with
        {
            Lessons = action.Lessons
        };

    [ReducerMethod]
    public static ShoppingCartState ReduceLocalStoreLoadError(ShoppingCartState state, LocalStoreLoadError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage }
        };


}
