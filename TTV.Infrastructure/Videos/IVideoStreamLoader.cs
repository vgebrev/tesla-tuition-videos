namespace TTV.Infrastructure.Videos
{
    public interface IVideoStreamLoader
    {
        Task<VideoInfo> LoadAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    }
}