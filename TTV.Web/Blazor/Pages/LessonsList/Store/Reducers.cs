using Fluxor;
using TTV.Web.Blazor.Pages.LessonsList.Store.Actions;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.LessonsList.Store;

public static class Reducers
{
    [ReducerMethod(typeof(GetTagsRequest))]
    public static LessonsListState ReduceGetTagsRequest(LessonsListState state) =>
        state with { IsLoadingTags = true };

    [ReducerMethod]
    public static LessonsListState ReduceGetTagsResponse(LessonsListState state, GetTagsResponse action) =>
        state with
        {
            Tags = action.Tags,
            TagsError = new(),
            IsLoadingTags = false
        };

    [ReducerMethod]
    public static LessonsListState ReduceGetTagsError(LessonsListState state, GetTagsError action) =>
        state with
        {
            TagsError = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoadingTags = false
        };

    [ReducerMethod]
    public static LessonsListState ReduceSetSearchTags(LessonsListState state, SetSearchTags action) =>
        state with { SearchTags = action.SearchTags };
    
    [ReducerMethod]
    public static LessonsListState ReduceSetFiltersVisibility(LessonsListState state, SetTagFilterDrawerVisibillity action) =>
        state with { IsTagFilterDrawerOpen = action.IsOpen };

    [ReducerMethod]
    public static LessonsListState ReduceSearchLessonsRequest(LessonsListState state, SearchLessonsRequest action) =>
        state with
        {
            SearchTags = action.SearchTags,
            SearchText = action.SearchText,
            IsTagFilterDrawerOpen = false,
            IsLoadingLessons = true
        };

    [ReducerMethod]
    public static LessonsListState ReduceSearchLessonsResponse(LessonsListState state, SearchLessonsResponse action)
    {
        return state with
        {
            Lessons = action.Lessons,
            LessonsError = new(),
            IsLoadingLessons = false
        };
    }

    [ReducerMethod]
    public static LessonsListState ReduceSearchLessonsError(LessonsListState state, SearchLessonsError action) =>
        state with
        {
            LessonsError = new ErrorState { IsError = true, ErrorMessage = action.ErrorMessage },
            IsLoadingLessons = false
        };

    [ReducerMethod]
    public static LessonsListState UpdateLessons(LessonsListState state, UpdateLessons action) =>
        state with { Lessons = state.Lessons?.Select(originalLesson => action.Lessons?.FirstOrDefault(updatedLesson => updatedLesson.Id == originalLesson.Id) ?? originalLesson).ToArray() };
}
