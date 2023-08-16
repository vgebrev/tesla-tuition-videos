using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DataContext dataContext;
    private readonly DbSet<TEntity> dbSet;
    public Repository(DataContext dataContext)
    {
        this.dataContext = dataContext;
        dbSet = dataContext.Set<TEntity>();
    }
    public virtual void Delete(TEntity entity)
    {
        if (dataContext.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await GetAsync(specification.Filters, specification.Sort, specification.IncludeProperties, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>[]? filters,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sort,
        string? includeProperties,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet.TagWith($"Getting list of {typeof(TEntity)} entities.").AsNoTracking();
        if (filters != null)
        {
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (sort != null)
        {
            return await sort(query).ToListAsync(cancellationToken);
        }
        else
        {
            return await query.ToListAsync(cancellationToken);
        }
    }

    public virtual async Task<TEntity?> GetAsync(int id, string? includeProperties, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet.TagWith($"Getting {typeof(TEntity)} by Id.").AsNoTracking();
        
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        dbSet.Attach(entity);
        dbSet.Entry(entity).State = EntityState.Modified;
    }
}
