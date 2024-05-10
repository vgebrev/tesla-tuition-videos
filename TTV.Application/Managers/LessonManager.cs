using TTV.Application.Exceptions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public class LessonManager(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentity) : ILessonManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;
    private readonly IUserIdentityService userIdentity = userIdentity;

    public async Task<IEnumerable<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.SearchAsync(searchText, searchTagsIds, userIdentity.UserId, cancellationToken);
        return lessons;
    }

    public async Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.LessonRepository.GetByIdAsync(lessonId, userIdentity.UserId, cancellationToken);
        return lesson;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsOwnedByUserAsync(CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsOwnedByUserAsync(userId, cancellationToken);
        return lessons;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(int[] lessonsIds, CancellationToken cancellationToken = default)
    {
        if (userIdentity.UserId == null)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsNotOwnedByUserAsync(userIdentity.UserId.Value, lessonsIds, cancellationToken);
        return lessons;
    }
}
