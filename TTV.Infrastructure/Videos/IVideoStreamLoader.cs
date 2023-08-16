namespace TTV.Infrastructure.Videos
{
    public interface IVideoStreamLoader
    {
        Task<Stream> LoadAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    }
}