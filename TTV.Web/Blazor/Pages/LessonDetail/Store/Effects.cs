using Fluxor;
using TTV.Web.Blazor.Pages.LessonDetail.Store.Actions;
using TTV.Web.Blazor.Pages.LessonsList.Store;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store;

public class Effects
{
    private readonly ILessonDataService dataService;
    private readonly IState<LessonsListState> lessonsState;

    public Effects(ILessonDataService dataService, IState<LessonsListState> lessonsState)
    {
        this.dataService = dataService;
        this.lessonsState = lessonsState;
    }

    [EffectMethod]
    public async Task HandleGetLessonRequest(GetLessonRequest action, IDispatcher dispatcher)
    {
        try
        {
            var lesson = (lessonsState.Value.Lessons?.SingleOrDefault(lesson => lesson.Id == action.LessonId)) ?? await dataService.GetLessonAsync(action.LessonId);
            dispatcher.Dispatch(new GetLessonResponse() { Lesson = lesson });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetLessonError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
