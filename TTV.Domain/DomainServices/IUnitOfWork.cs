using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices;

public interface IUnitOfWork : IDisposable
{
    public Task StartAsync();
    public Task EndAsync();
    public IRepository<TEntity> GetRepository<TEntity>() 
        where TEntity : BaseEntity;
}
