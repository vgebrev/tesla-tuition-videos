using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Domain.Specifications;

namespace TTV.Application.DataServices;

public class LessonDataService : ILessonDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private static readonly string fullGraph = $"{nameof(Lesson.Tags)}.{nameof(Tag.Category)},{nameof(Lesson.Video)}";

    public LessonDataService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.GetRepository<Lesson>().GetAsync(includeProperties: fullGraph, cancellationToken: cancellationToken);
        return lessons;
    }

    public async Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.GetRepository<Lesson>()
            .GetAsync(lessonId,
                includeProperties: fullGraph,
                cancellationToken: cancellationToken);

        return lesson;
    }


    public async Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.GetRepository<Lesson>().GetAsync(new LessonSearchSpecification(searchText, searchTagsIds), cancellationToken: cancellationToken);
        return lessons;
    }
}
