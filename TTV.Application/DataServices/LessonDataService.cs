using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public class LessonDataService : ILessonDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    public LessonDataService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.LessonRepository.GetByIdAsync(lessonId, cancellationToken);
        return lesson;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsOwnedByCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsOwnedByCurrentUserAsync(cancellationToken);
        return lessons;
    }

    public async Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.SearchAsync(searchText, searchTagsIds, cancellationToken);
        return lessons;
    }
}
