using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class LessonRepository(DataContext dataContext) : ILessonRepository
{
    private readonly DataContext dataContext = dataContext;

    public async Task<IEnumerable<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, Guid? ownerId, CancellationToken cancellationToken = default)
    {
        var query = dataContext.Lessons.TagWithCallSite()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(lesson => lesson.Title.Contains(searchText) || lesson.Description.Contains(searchText) || lesson.Tags.Any(tag => tag.Name.Contains(searchText)));
        }

        if (searchTagsIds != null && searchTagsIds.Length > 0)
        {
            query = query.Where(lesson => lesson.Tags.Any(tag => searchTagsIds.Contains(tag.Id)));
        }

        query = query.Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Lesson?> GetByIdAsync(int lessonId, Guid? ownerId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Lessons.TagWithCallSite()
            .AsNoTracking()
            .Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId))
            .SingleOrDefaultAsync(lesson => lesson.Id == lessonId, cancellationToken);
    }

    public async Task<IEnumerable<Lesson>> GetLessonsOwnedByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Lessons.TagWithCallSite()
            .AsNoTracking()
            .Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Where(lesson => lesson.OwnedBy.Any(user => user.Id == userId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(Guid userId, int[] lessonsIds, CancellationToken cancellationToken = default)
    {
        return await dataContext.Lessons.TagWithCallSite()
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Where(lesson => lessonsIds.Contains(lesson.Id) && !lesson.OwnedBy.Any(user => user.Id == userId))
            .ToListAsync(cancellationToken);
    }
}
