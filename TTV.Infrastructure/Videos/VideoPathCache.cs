using System.Collections.Concurrent;

namespace TTV.Infrastructure.Videos;

public class VideoPathCache : IVideoPathCache
{
    private readonly ConcurrentDictionary<(int, string?), string> cache = new();

    public bool TryAdd(int lessonId, string? userEmail, string path)
    {
        return cache.TryAdd((lessonId, userEmail), path);
    }

    public bool TryRemove(int lessonId, string? userEmail)
    {
        return cache.TryRemove((lessonId, userEmail), out _);
    }

    public bool TryGetPath(int lessonId, string? userEmail, out string path)
    {
        return cache.TryGetValue((lessonId, userEmail), out path!);

    }
}
