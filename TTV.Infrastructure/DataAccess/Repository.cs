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

    public virtual async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
    {
        return await GetAsync(specification.Filter, specification.Sort, specification.IncludeProperties);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sort,
        string? includeProperties)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
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
            return await sort(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task<TEntity?> GetAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        dbSet.Attach(entity);
        dbSet.Entry(entity).State = EntityState.Modified;
    }
}
