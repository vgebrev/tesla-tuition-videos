using TTV.Domain.Entities;

namespace TTV.Application.DataServices
{
    public interface IVideoDataService
    {
        Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
    }
}