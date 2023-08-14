using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private bool disposed = false;
    private IDbContextTransaction? transaction;
    private readonly DataContext dataContext;
    private readonly Hashtable repositories;

    public UnitOfWork(DataContext dataContext)
    {
        this.dataContext = dataContext;
        repositories = new Hashtable();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                dataContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task EndAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dataContext.SaveChangesAsync(cancellationToken);
            if (transaction is not null)
                await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction is not null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var entityType = typeof(TEntity);
        if (repositories.ContainsKey(entityType))
            return (IRepository<TEntity>?)repositories[entityType] ?? throw new InvalidCastException();

        var repositoryType = typeof(Repository<>);
        var repositoryInstance = (IRepository<TEntity>?)Activator.CreateInstance(repositoryType.MakeGenericType(entityType), dataContext) ?? throw new InvalidOperationException();
        repositories.Add(entityType, repositoryInstance);
        return (IRepository<TEntity>?)repositories[entityType] ?? throw new InvalidCastException();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        transaction = await this.dataContext.Database.BeginTransactionAsync(cancellationToken);
    }
}
