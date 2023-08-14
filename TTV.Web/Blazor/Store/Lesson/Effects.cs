using Fluxor;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Store.Lesson.Actions;
using TTV.Web.Blazor.Store.Lessons;

namespace TTV.Web.Blazor.Store.Lesson;

public class Effects
{
    private readonly ILessonDataService dataService;
    private readonly IState<LessonsState> lessonsState;

    public Effects(ILessonDataService dataService, IState<LessonsState> lessonsState)
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
