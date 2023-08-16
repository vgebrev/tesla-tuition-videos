using TTV.Domain.Entities;

namespace TTV.Application.DataServices
{
    public interface IVideoDataService
    {
        Task<Video> GetVideoAsync(int videoId, CancellationToken cancellationToken = default);
    }
}