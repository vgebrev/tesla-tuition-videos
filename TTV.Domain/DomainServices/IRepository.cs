using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices;

public interface IRepository<TEntity> 
    where TEntity : BaseEntity
{
    public Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    public Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>[]? filters = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sort = null,
        string? includeProperties = null,
        CancellationToken cancellationToken = default);

    public Task<TEntity?> GetAsync(int id, string? includeProperties, CancellationToken cancellationToken = default);

    public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    public void Delete(TEntity entity);

    public void Update(TEntity entity);
}
