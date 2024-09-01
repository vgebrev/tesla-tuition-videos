using TTV.Domain.DomainServices;
using TTV.Domain.Entities;


namespace TTV.Application.Managers;
public class UserManager(IUnitOfWorkFactory unitOfWorkFactory) : IUserManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;

    public async Task<User[]> GetAllsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var users = await unitOfWork.UserRepository.GetAllAsync(cancellationToken);
        return [.. users.OrderBy(user => user.Name)];
    }
}
