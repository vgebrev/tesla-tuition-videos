namespace TTV.Infrastructure.Video
{
    public interface IVideoStreamLoader
    {
        Task<VideoInfo> LoadAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    }
}