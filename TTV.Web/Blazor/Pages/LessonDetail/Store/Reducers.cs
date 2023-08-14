using Fluxor;
using TTV.Web.Blazor.Pages.LessonDetail.Store.Actions;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store;

public static class Reducers
{
    [ReducerMethod(typeof(GetLessonRequest))]
    public static LessonDetailState ReduceGetLessonRequest(LessonDetailState state) =>
        state with
        {
            IsLoading = true,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetLessonResponse(LessonDetailState state, GetLessonResponse action) =>
        state with
        {
            Lesson = action.Lesson,
            Error = new(),
            IsLoading = false,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetLessonError(LessonDetailState state, GetLessonError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoading = false,
        };
}
