using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTV.Application;
using TTV.Application.Managers;

namespace TTV.Infrastructure.Videos;

public class VideoStreamLoader(ILogger<VideoStreamLoader> logger, IVideoPathCache videoPathCache, IVideoManager videoManager, IOptionsSnapshot<SystemSettings> config) : IVideoStreamLoader
{
    private readonly ILogger<VideoStreamLoader> logger = logger;
    private readonly IVideoPathCache videoPathCache = videoPathCache;
    private readonly IVideoManager videoManager = videoManager;
    private readonly SystemSettings settings = config.Value;

    public async Task<Stream> LoadLessonVideoStreamAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({LessonID}, {UserEmail})", nameof(LoadLessonVideoStreamAsync), lessonId, userEmail);
        var path = await GetVideoPathAsync(lessonId, userEmail, cancellationToken);
        if (path == null)
        {
            logger.LogDebug("Path is null. Returning Stream.Null");
            return Stream.Null;
        }
        var stream = File.OpenRead(path);
        return stream;
    }

    /// <remarks>Loads path from cache once it's loaded from Database, to avoid querying the database for every chunk of stream the client requests.</remarks>
    private async Task<string?> GetVideoPathAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        if (videoPathCache.TryGetPath(lessonId, userEmail, out var cachedPath))
        {
            logger.LogTrace("Path for lesson {LessonId} and user {UserEmail} found in cache", lessonId, userEmail);
            return cachedPath;
        }
        else
        {
            logger.LogTrace("Path for lesson {LessonId} and user {UserEmail} not found in cache. Getting from database", lessonId, userEmail);
            var video = await videoManager.GetLessonVideoForUserAsync(lessonId, userEmail, cancellationToken);
            if (video == null)
            {
                return null;
            }
            var videoPath = Path.Combine(settings.VideosPath, video.RelativePath, video.Filename);
            videoPathCache.TryAdd(lessonId, userEmail, videoPath);
            return videoPath;
        }
    }

    public async Task<Stream> LoadThumbnailStreamAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        var video = await videoManager.GetLessonVideoForUserAsync(lessonId, userEmail, cancellationToken);
        if (video == null)
        {
            return Stream.Null;
        }
        var path = Path.Combine(settings.VideosPath, video.RelativePath, "Thumbnails", video.Thumbnail);
        return File.OpenRead(path);
    }
}
