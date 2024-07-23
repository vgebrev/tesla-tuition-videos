using Fluxor;
using TTV.Web.Blazor.Pages.LessonsList.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.LessonsList.Store;

public class Effects(ITagApiConsumer tagApi, ILessonApiConsumer lessonApi)
{
    private readonly ITagApiConsumer tagApi = tagApi;
    private readonly ILessonApiConsumer lessonApi = lessonApi;

    [EffectMethod(typeof(GetTagsRequest))]
    public async Task HandleGetTagsRequest(IDispatcher dispatcher)
    {
        try
        {
            var tags = await tagApi.GetTagsAsync();
            dispatcher.Dispatch(new GetTagsResponse() { Tags = tags });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetTagsError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod]
    public async Task HandleSearchLessonsRequest(SearchLessonsRequest action, IDispatcher dispatcher)
    {
        try
        {
            var lessons = await lessonApi.SearchLessonsAsync(new LessonSearchDto()
            {
                SearchTagsIds = action.SearchTags?.Select(tag => tag.Id).ToArray(),
                SearchText = action.SearchText
            });
            dispatcher.Dispatch(new SearchLessonsResponse() { Lessons = lessons });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new SearchLessonsError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod]
    public static async Task HandleSetSearchTags(SetSearchTags action, IDispatcher dispatcher)
    {
        if (action.AutoSearch)
        {
            dispatcher.Dispatch(new SearchLessonsRequest() { SearchTags = action.SearchTags });
        }
        await Task.CompletedTask;
    }
}
