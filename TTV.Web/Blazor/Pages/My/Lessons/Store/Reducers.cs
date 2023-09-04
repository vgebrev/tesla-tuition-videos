using Fluxor;
using TTV.Web.Blazor.Pages.My.Lessons.Store.Actions;

namespace TTV.Web.Blazor.Pages.My.Lessons.Store;

public static class Reducers
{
    [ReducerMethod(typeof(GetLessonsRequest))]
    public static MyLessonsState ReduceGetLessonsRequest(MyLessonsState state) =>
        state with
        {
            IsLoading = true,
            Error = new()
        };

    [ReducerMethod]
    public static MyLessonsState ReduceGetLessonsResponse(MyLessonsState state, GetLessonsResponse action) =>
        state with
        {
            Lessons = action.Lessons,
            IsLoading = false,
            Error = new()
        };

    [ReducerMethod]
    public static MyLessonsState ReduceGetLessonsError(MyLessonsState state, GetLessonsError action) =>
        state with
        {
            IsLoading = false,
            Error = new() { IsError = true, ErrorMessage = action.ErrorMessage }
        };
}
