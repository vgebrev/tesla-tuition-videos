using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly DataContext dataContext;

    public VideoRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
    public async Task<Video?> GetByIdAsync(int videoId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Videos.TagWithCallSite()
            .Include(video => video.Intro)
            .SingleOrDefaultAsync(video => video.Id == videoId, cancellationToken);
    }
}
