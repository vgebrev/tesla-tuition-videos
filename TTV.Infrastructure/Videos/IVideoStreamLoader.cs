namespace TTV.Infrastructure.Videos;

public interface IVideoStreamLoader
{
    Task<Stream> LoadLessonVideoStreamAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    Task<Stream> LoadThumbnailStreamAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
}