using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;
public class LearningPathRepository(DataContext dataContext) : ILearningPathRepository
{
    private readonly DataContext dataContext = dataContext;
    public async Task<IEnumerable<LearningPath>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var result = await dataContext.LearningPaths.TagWithCallSite()
            .Include(p => p.Items)
            .ThenInclude(i => i.Lesson)
            .ToListAsync(cancellationToken);

        var items = result.SelectMany(p => p.Items.OfType<LearningPathItem>()).ToList();
        foreach (var item in items)
        {
            await LoadItems(item);
        }
        return result;
    }

    private async Task LoadItems(LearningPathItem item)
    {
        await dataContext.Entry(item)
            .Collection(i => i.Items)
            .Query().TagWithCallSite()
            .Include(i => i.Lesson)
            .Include(i => i.Items)
            .LoadAsync();

        foreach (var childItem in item.Items.OfType<LearningPathItem>())
        {
            await LoadItems(childItem);
        }
    }
}