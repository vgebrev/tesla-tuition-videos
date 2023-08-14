using Fluxor;
using TTV.Web.Blazor.Store.Lesson.Actions;

namespace TTV.Web.Blazor.Store.Lesson;

public static class Reducers
{
    [ReducerMethod(typeof(GetLessonRequest))]
    public static LessonState ReduceGetLessonRequest(LessonState state) =>
        state with
        {
            IsLoading = true,
        };

    [ReducerMethod]
    public static LessonState ReduceGetLessonResponse(LessonState state, GetLessonResponse action) =>
        state with
        {
            Lesson = action.Lesson,
            Error = new(),
            IsLoading = false,
        };

    [ReducerMethod]
    public static LessonState ReduceGetLessonError(LessonState state, GetLessonError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoading = false,
        };
}
