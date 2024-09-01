using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface IUserManager
{
    Task<User[]> GetAllsync(CancellationToken cancellationToken = default);
}