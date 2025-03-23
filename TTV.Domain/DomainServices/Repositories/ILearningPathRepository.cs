using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;
public interface ILearningPathRepository
{
    Task<IEnumerable<LearningPath>> GetAllAsync(CancellationToken cancellationToken = default);
}
