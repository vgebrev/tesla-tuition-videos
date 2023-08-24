using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext dataContext;

    public UserRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Users.TagWithCallSite()
            .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}
