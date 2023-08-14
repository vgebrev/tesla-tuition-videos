using Fluxor;
using TTV.Web.Blazor.Store.Lessons.Actions;

namespace TTV.Web.Blazor.Store.Lessons;

public static class Reducers
{
    [ReducerMethod(typeof(GetTagsRequest))]
    public static LessonsState ReduceGetTagsRequest(LessonsState state) =>
        state with { IsLoadingTags = true };

    [ReducerMethod]
    public static LessonsState ReduceGetTagsResponse(LessonsState state, GetTagsResponse action) =>
        state with
        {
            Tags = action.Tags,
            TagsError = new(),
            IsLoadingTags = false
        };

    [ReducerMethod]
    public static LessonsState ReduceGetTagsError(LessonsState state, GetTagsError action) =>
        state with
        {
            TagsError = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoadingTags = false
        };

    [ReducerMethod]
    public static LessonsState ReduceSetSearchTags(LessonsState state, SetSearchTags action) =>
        state with { SearchTags = action.SearchTags };

    [ReducerMethod]
    public static LessonsState ReduceSearchLessonsRequest(LessonsState state, SearchLessonsRequest action) =>
        state with
        {
            SearchTags = action.SearchTags,
            SearchText = action.SearchText,
            IsLoadingLessons = true
        };

    [ReducerMethod]
    public static LessonsState ReduceSearchLessonsResponse(LessonsState state, SearchLessonsResponse action) {
        return state with
        {
            Lessons = action.Lessons,
            LessonsError = new(),
            IsLoadingLessons = false
        };
    }

    [ReducerMethod]
    public static LessonsState ReduceSearchLessonsError(LessonsState state, SearchLessonsError action) =>
        state with
        {
            LessonsError = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoadingLessons = false
        };
}
