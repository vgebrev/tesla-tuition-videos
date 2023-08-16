using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public class VideoDataService : IVideoDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    public VideoDataService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<Video?> GetVideoAsync(int videoId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var video = await unitOfWork.GetRepository<Video>()
            .GetAsync(videoId, includeProperties: $"{nameof(Video.Intro)}", cancellationToken: cancellationToken);
        return video;
    }
}
