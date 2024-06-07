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
            IsLessonLoading = true,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetLessonResponse(LessonDetailState state, GetLessonResponse action) =>
        state with
        {
            Lesson = action.Lesson,
            Error = new(),
            IsLessonLoading = false,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetLessonError(LessonDetailState state, GetLessonError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLessonLoading = false,
        };

    [ReducerMethod(typeof(GetDocumentsRequest))]
    public static LessonDetailState ReduceGetDocumentsRequest(LessonDetailState state) =>
        state with
        {
            IsDocumentsLoading = true,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetDocumentsResponse(LessonDetailState state, GetDocumentsResponse action) =>
        state with
        {
            Documents = action.Documents,
            Error = new(),
            IsDocumentsLoading = false,
        };

    [ReducerMethod]
    public static LessonDetailState ReduceGetDocumentsError(LessonDetailState state, GetDocumentsError action) =>
        state with
        {
            Error = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsDocumentsLoading = false,
        };
}
