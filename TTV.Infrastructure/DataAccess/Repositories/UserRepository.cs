using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class UserRepository(DataContext dataContext) : IUserRepository
{
    private readonly DataContext dataContext = dataContext;

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Users.TagWithCallSite()
            .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}
