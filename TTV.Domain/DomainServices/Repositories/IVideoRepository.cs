using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IVideoRepository
{

    /// <remarks>
    /// Return the Intro video for the lesson if the user is anonymous (null) or doesn't own the lesson.
    /// Return the Full Lesson video if the user owns the lesson or the lesson is free.
    /// </remarks>
    Task<Video?> GetLessonVideoForUserAsync(int lessonId, string? userEmail, CancellationToken cancellationToken = default);
}
