using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public class LessonDataService : ILessonDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IUserIdentityService userIdentityService;

    public LessonDataService(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentityService)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.userIdentityService = userIdentityService;
    }

    public async Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.SearchAsync(searchText, searchTagsIds, userIdentityService.UserId, cancellationToken);
        return lessons;
    }

    public async Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.LessonRepository.GetByIdAsync(lessonId, userIdentityService.UserId, cancellationToken);
        return lesson;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsOwnedByUserAsync(CancellationToken cancellationToken = default)
    {
        if (userIdentityService.UserId == null)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsOwnedByUserAsync(userIdentityService.UserId.Value, cancellationToken);
        return lessons;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(int[] lessonsIds, CancellationToken cancellationToken = default)
    {
        if (userIdentityService.UserId == null)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsNotOwnedByUserAsync(userIdentityService.UserId.Value, lessonsIds, cancellationToken);
        return lessons;
    }
}
