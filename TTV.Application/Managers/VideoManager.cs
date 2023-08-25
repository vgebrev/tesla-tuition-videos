using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public class VideoManager : IVideoManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    public VideoManager(IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var video = await unitOfWork.VideoRepository.GetLessonVideoForUserAsync(lessonId, userEmail, cancellationToken);
        return video;
    }
}
