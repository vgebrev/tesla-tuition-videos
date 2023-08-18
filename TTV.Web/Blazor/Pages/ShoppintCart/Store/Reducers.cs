using Fluxor;
using TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ShoppingCartState AddLessonToCart(ShoppingCartState state, AddLesson action) =>
        state with
        {
            Lessons = new List<LessonDto>(state.Lessons.Union(new[] { action.Lesson })).ToArray()
        };

    [ReducerMethod]
    public static ShoppingCartState RemoveLessonFromCart(ShoppingCartState state, RemoveLesson action) =>
        state with
        {
            Lessons = new List<LessonDto>(state.Lessons.Where(lesson => lesson != action.Lesson)).ToArray()
        };
}
