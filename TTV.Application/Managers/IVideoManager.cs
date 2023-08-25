using TTV.Domain.Entities;

namespace TTV.Application.Managers
{
    public interface IVideoManager
    {
        Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    }
}