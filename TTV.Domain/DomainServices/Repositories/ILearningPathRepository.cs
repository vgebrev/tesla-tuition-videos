using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;
public interface ILearningPathRepository
{
    Task<IEnumerable<LearningPath>> GetListAsync(CancellationToken cancellationToken = default);
}
