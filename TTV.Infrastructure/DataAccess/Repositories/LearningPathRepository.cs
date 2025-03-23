using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;
public class LearningPathRepository(DataContext dataContext) : ILearningPathRepository
{
    private readonly DataContext dataContext = dataContext;
    public async Task<IEnumerable<LearningPath>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await dataContext.LearningPaths.TagWithCallSite()
           .AsNoTracking()
           .Include(p => p.Items)
           .ToListAsync(cancellationToken);

        foreach (var item in result.SelectMany(p => p.Items.OfType<LearningPathItem>()))
        {
            await LoadItems(item);
        }
        return result;
    }

    private async Task LoadItems(LearningPathItem item)
    {
        await dataContext.Entry(item)
            .Collection(i => i.Items)
            .Query().TagWithCallSite().AsNoTracking()
            .Include(i => i.Lesson)
            .Include(i => i.Items)
            .LoadAsync();

        foreach (var childItem in item.Items.OfType<LearningPathItem>())
        {
            await LoadItems(childItem);
        }
    }
}