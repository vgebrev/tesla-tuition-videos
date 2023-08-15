using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TTV.Infrastructure.Videos;

public class VideoStreamLoader : IVideoStreamLoader
{
    private readonly ILogger<VideoStreamLoader> logger;
    private readonly FileSystemSettings settings;

    public VideoStreamLoader(ILogger<VideoStreamLoader> logger, IOptionsSnapshot<FileSystemSettings> config)
    {
        this.logger = logger;
        this.settings = config.Value;
    }
    public async Task<VideoInfo> LoadAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default)
    {
        // TODO: Async I/O
        await Task.CompletedTask; 
        logger.LogInformation("{MethodName}({LessonID}, {UserEmail})", nameof(LoadAsync), lessonId, userEmail);
        // TODO: Link to lessons and ownership to determine whether to serve the full lesson or just the intro
        var filename = string.IsNullOrEmpty(userEmail) ? "TTV-Intro-Placeholder.mp4" : "TTV-Lesson-Placeholder.mp4";
        var path = Path.Combine(settings.VideosPath, filename);
        var stream = File.OpenRead(path);
        return new VideoInfo
        {
            Id = lessonId,
            Filename = filename,
            Stream = stream,
        };
    }
}
