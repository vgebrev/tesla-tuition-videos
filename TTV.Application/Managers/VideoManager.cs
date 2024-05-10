using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public class VideoManager(IUnitOfWorkFactory unitOfWorkFactory) : IVideoManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;

    public async Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var video = await unitOfWork.VideoRepository.GetLessonVideoForUserAsync(lessonId, userEmail, cancellationToken);
        return video;
    }
}
