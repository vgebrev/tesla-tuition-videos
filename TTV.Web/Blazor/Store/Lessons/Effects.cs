using Fluxor;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Store.Lessons.Actions;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Store.Lessons;

public class Effects
{
    private readonly ITagDataService tagDataService;
    private readonly ILessonDataService lessonDataService;

    public Effects(ITagDataService tagDataService, ILessonDataService lessonDataService)
    {
        this.tagDataService = tagDataService;
        this.lessonDataService = lessonDataService;
    }

    [EffectMethod(typeof(GetTagsRequest))]
    public async Task HandleGetTagsRequest(IDispatcher dispatcher)
    {
        var tags = await tagDataService.GetTagsAsync();
        dispatcher.Dispatch(new GetTagsResponse() { Tags = tags });
    }

    [EffectMethod]
    public async Task HandleSearchLessonsRequest(SearchLessonsRequest action, IDispatcher dispatcher)
    {
        var lessons = await lessonDataService.SearchLessonsAsync(new SearchLessonsDto()
        {
            SearchTagsIds = action.SearchTags?.Select(tag => tag.Id).ToArray(),
            SearchText = action.SearchText
        });
        dispatcher.Dispatch(new SearchLessonsResponse() { Lessons = lessons });
    }
}
