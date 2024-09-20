using TTV.Application.Exceptions;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Application.Managers;

public class LessonManager(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentity) : ILessonManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;
    private readonly IUserIdentityService userIdentity = userIdentity;

    public async Task<Page<Lesson>> SearchLessonsAsync(string? searchText, int[]? searchTagsIds, PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.SearchAsync(searchText, searchTagsIds, userIdentity.UserId, pageFilter, cancellationToken);
        return lessons;
    }

    public async Task<Lesson?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.LessonRepository.GetByIdAsync(lessonId, userIdentity.UserId, cancellationToken);
        return lesson;
    }

    public async Task<Page<Lesson>> GetLessonsOwnedByUserAsync(PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.LessonRepository.GetLessonsOwnedByUserAsync(userId, pageFilter, cancellationToken);
        return lessons;
    }
}
