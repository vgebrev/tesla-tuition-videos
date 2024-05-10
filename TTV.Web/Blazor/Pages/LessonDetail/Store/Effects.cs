using Fluxor;
using TTV.Web.Blazor.Pages.LessonDetail.Store.Actions;
using TTV.Web.Blazor.Pages.LessonsList.Store;
using TTV.Web.Blazor.Services;
using TTV.Web.Blazor.Shared.Store;

namespace TTV.Web.Blazor.Pages.LessonDetail.Store;

public class Effects(ILessonApiConsumer lessonApi, IDocumentApiConsumer documentApi, IState<LessonsListState> lessonsState)
{
    [EffectMethod]
    public async Task HandleGetLessonRequest(GetLessonRequest action, IDispatcher dispatcher)
    {
        try
        {
            var lesson = (lessonsState.Value.Lessons?.SingleOrDefault(lesson => lesson.Id == action.LessonId)) ?? await lessonApi.GetLessonAsync(action.LessonId);
            dispatcher.Dispatch(new GetLessonResponse() { Lesson = lesson });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetLessonError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }

    [EffectMethod]
    public async Task HandleGetDocumentsRequest(GetDocumentsRequest action, IDispatcher dispatcher)
    {
        try
        {
            var documents = await documentApi.GetDocumentsForLessonOwnedByCurrentUserAsync(action.LessonId);
            dispatcher.Dispatch(new GetDocumentsResponse() { Documents = documents });
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new GetDocumentsError() { ErrorMessage = Consts.DefaultErrorMessage });
        }
    }
}
