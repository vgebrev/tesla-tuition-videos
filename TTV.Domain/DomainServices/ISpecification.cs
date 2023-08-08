using System.Linq.Expressions;
using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices
{
    public interface ISpecification<TEntity>
        where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Sort { get; set; }
        public string IncludeProperties { get; set; }
    }
}
