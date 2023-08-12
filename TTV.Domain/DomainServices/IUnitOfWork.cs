using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices;

public interface IUnitOfWork : IDisposable
{
    public Task StartAsync(CancellationToken cancellationToken = default);
    public Task EndAsync(CancellationToken cancellationToken = default);
    public IRepository<TEntity> GetRepository<TEntity>() 
        where TEntity : BaseEntity;
}
