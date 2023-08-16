using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTV.Application.DataServices;

namespace TTV.Infrastructure.Videos;

public class VideoStreamLoader : IVideoStreamLoader
{
    private readonly ILogger<VideoStreamLoader> logger;
    private readonly IVideoPathCache videoPathCache;
    private readonly IVideoDataService videoDataService;
    private readonly FileSystemSettings settings;

    public VideoStreamLoader(ILogger<VideoStreamLoader> logger, IVideoPathCache videoPathCache, IVideoDataService videoDataService, IOptionsSnapshot<FileSystemSettings> config)
    {
        this.logger = logger;
        this.videoPathCache = videoPathCache;
        this.videoDataService = videoDataService;
        settings = config.Value;
    }

    /// <remarks>Loads path from cache, to avoid querying the database for every chunk of stream the client requests.</remarks>
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

    private async Task<string?> GetVideoPathAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        if (videoPathCache.TryGetPath(lessonId, userEmail, out var cachedPath))
        {
            logger.LogDebug("Path for lesson {LessonId} and user {UserEmail} found in cache", lessonId, userEmail);
            return cachedPath;
        }
        else
        {
            logger.LogDebug("Path for lesson {LessonId} and user {UserEmail} not found in cache. Getting from database", lessonId, userEmail);
            var video = await videoDataService.GetLessonVideoForUserAsync(lessonId, userEmail, cancellationToken);
            if (video == null)
            {
                return null;
            }
            var videoPath = Path.Combine(settings.VideosPath, video.RelativePath, video.Filename);
            videoPathCache.TryAdd(lessonId, userEmail, videoPath);
            return videoPath;
        }
    }
}
