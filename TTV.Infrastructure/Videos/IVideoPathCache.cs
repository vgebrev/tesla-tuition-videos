namespace TTV.Infrastructure.Videos
{
    public interface IVideoPathCache
    {
        bool TryAdd(int lessonId, string? userEmail, string path);
        bool TryGetPath(int lessonId, string? userEmail, out string path);
        bool TryRemove(int lessonId, string? userEmail);
    }
}