using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IVideoRepository
{
    Task<Video?> GetByIdAsync(int videoId, CancellationToken cancellationToken = default);
}
