using System.Linq.Expressions;
using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices;

public interface IRepository<TEntity> 
    where TEntity : BaseEntity
{
    public Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification);

    public Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sort = null,
        string? includeProperties = null);

    public Task<TEntity?> GetAsync(int id);

    public Task InsertAsync(TEntity entity);

    public void Delete(TEntity entity);

    public void Update(TEntity entity);
}
