using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public class LearningPathManager(IUnitOfWorkFactory unitOfWorkFactory) : ILearningPathManager
{
    public async Task<IEnumerable<LearningPath>> GetallAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        return await unitOfWork.LearningPathRepository.GetListAsync(cancellationToken);
    }
}
