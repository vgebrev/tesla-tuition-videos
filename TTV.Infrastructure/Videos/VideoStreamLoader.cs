using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTV.Application.DataServices;

namespace TTV.Infrastructure.Videos;

public class VideoStreamLoader : IVideoStreamLoader
{
    private readonly ILogger<VideoStreamLoader> logger;
    private readonly IVideoDataService dataService;
    private readonly FileSystemSettings settings;

    public VideoStreamLoader(ILogger<VideoStreamLoader> logger, IVideoDataService dataService, IOptionsSnapshot<FileSystemSettings> config)
    {
        this.logger = logger;
        this.dataService = dataService;
        this.settings = config.Value;
    }
    public async Task<VideoInfo> LoadAsync(int videoId, string? userEmail, CancellationToken cancellationToken = default)
    {        
        logger.LogInformation("{MethodName}({LessonID}, {UserEmail})", nameof(LoadAsync), videoId, userEmail);
        var video = await dataService.GetVideoAsync(videoId, cancellationToken);
        // TODO: Link to lessons and ownership to determine whether to serve the full lesson or just the intro
        var chosenVideo = string.IsNullOrEmpty(userEmail) ? video.Intro : video;
        var path = Path.Combine(settings.VideosPath, chosenVideo?.RelativePath ?? string.Empty, chosenVideo?.Filename ?? string.Empty);
        var stream = File.OpenRead(path);
        return new VideoInfo
        {
            Id = videoId,
            Filename = chosenVideo?.Filename ?? "Video.mp4",
            Stream = stream,
        };
    }
}
