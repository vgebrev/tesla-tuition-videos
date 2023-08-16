using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DataContext dataContext;

    public TagRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
    public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dataContext.Tags.TagWithCallSite()
            .AsNoTracking()
            .Include(tag => tag.Category)
            .ToListAsync(cancellationToken);
    }
}
