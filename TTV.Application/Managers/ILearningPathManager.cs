using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface ILearningPathManager
{
   public Task<IEnumerable<LearningPath>> GetallAsync(CancellationToken cancellationToken = default);
}
