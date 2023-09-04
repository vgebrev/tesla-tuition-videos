using Fluxor;
using TTV.Web.Blazor.Pages.My.Lessons.Store.Actions;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.My.Lessons.Store;

public class Effects
{
    private readonly ILessonApiConsumer lessonApi;

    public Effects(ILessonApiConsumer lessonApi)
    {
        this.lessonApi = lessonApi;
    }

    [EffectMethod(typeof(GetLessonsRequest))]
    public async Task HandleGetLessonsRequest(IDispatcher dispatcher)
    {
        try
        {
            var lessons = await lessonApi.GetLessonsOwnedByCurrentUserAsync();
            dispatcher.Dispatch(new GetLessonsResponse() { Lessons = lessons });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetLessonsError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
