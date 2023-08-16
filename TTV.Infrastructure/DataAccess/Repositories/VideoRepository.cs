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

    public async Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        var query = dataContext.Videos.TagWithCallSite()
            .AsNoTracking()
            .Include(video => video.Lesson)
            .ThenInclude(lesson => lesson.OwnedBy)
            .Where(video => video.Lesson.Id == lessonId)
            .Where(video => video.VideoType == VideoType.Intro || video.Lesson.OwnedBy.Any(user => user.Email == userEmail));

        var videos = await query.Select(video => video).ToListAsync(cancellationToken);

        var intro = videos.FirstOrDefault(video => video.VideoType == VideoType.Intro);
        var fullLesson = videos.FirstOrDefault(video => video.VideoType == VideoType.FullLesson);

        return fullLesson ?? intro;
    }
}
