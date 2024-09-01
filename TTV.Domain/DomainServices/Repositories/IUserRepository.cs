using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<User[]> GetAllAsync(CancellationToken cancellationToken = default);
}
