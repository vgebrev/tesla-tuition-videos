using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices;

namespace TTV.Infrastructure.DataAccess;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<DataContext> dbContextFactory;
    private readonly IUserIdentityService userIdentityService;

    public UnitOfWorkFactory(IDbContextFactory<DataContext> dbContextFactory, IUserIdentityService userIdentityService)
    {
        this.dbContextFactory = dbContextFactory;
        this.userIdentityService = userIdentityService;
    }
    public async Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
    {
        return new UnitOfWork(await dbContextFactory.CreateDbContextAsync(cancellationToken), userIdentityService);
    }
}
