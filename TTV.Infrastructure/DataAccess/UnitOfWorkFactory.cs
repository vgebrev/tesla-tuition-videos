using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTV.Domain.DomainServices;

namespace TTV.Infrastructure.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;

        public UnitOfWorkFactory(IDbContextFactory<DataContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
        {
            return new UnitOfWork(await dbContextFactory.CreateDbContextAsync(cancellationToken));
        }
    }
}
