using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
}
