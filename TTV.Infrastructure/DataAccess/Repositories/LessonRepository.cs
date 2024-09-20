using Microsoft.EntityFrameworkCore;
using TTV.Domain;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;
using TTV.Domain.Filters;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class LessonRepository(DataContext dataContext) : ILessonRepository
{
    private readonly DataContext dataContext = dataContext;

    public async Task<Page<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, Guid? ownerId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
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
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == ownerId))
            .OrderBy(lesson => lesson.Id);

        PageInfo? pageInfo = null;
        if (pageFilter is not null)
        {
            var count = await query.CountAsync(cancellationToken);
            query = (IOrderedQueryable<Lesson>)query.Skip(pageFilter.Skip ?? 0).Take(pageFilter.Take ?? count);
            pageInfo = new PageInfo()
            {
                Skip = pageFilter.Skip ?? 0,
                Take = pageFilter.Take ?? count,
                Count = count
            };
        }
        return new Page<Lesson>()
        {
            Items = await query.ToListAsync(cancellationToken),
            PageInfo = pageInfo
        };
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

    public async Task<Page<Lesson>> GetLessonsOwnedByUserAsync(Guid userId, PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        var query = dataContext.Lessons.TagWithCallSite()
            .AsNoTracking()
            .Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Where(lesson => lesson.OwnedBy.Any(user => user.Id == userId) || lesson.IsFree)
            .OrderBy(lesson => lesson.Id);

        PageInfo? pageInfo = null;
        if (pageFilter is not null)
        {
            var count = await query.CountAsync(cancellationToken);
            query = (IOrderedQueryable<Lesson>)query.Skip(pageFilter.Skip ?? 0).Take(pageFilter.Take ?? count);
            pageInfo = new PageInfo()
            {
                Skip = pageFilter.Skip ?? 0,
                Take = pageFilter.Take ?? count,
                Count = count
            };
        }

        return new Page<Lesson>()
        {
            Items = await query.ToListAsync(cancellationToken),
            PageInfo = pageInfo
        };
    }

    public async Task<IEnumerable<Lesson>> GetLessonsNotOwnedByUserAsync(Guid userId, int[] lessonsIds, CancellationToken cancellationToken = default)
    {
        return await dataContext.Lessons.TagWithCallSite()
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == userId))
            .Where(lesson => lessonsIds.Contains(lesson.Id) && !lesson.OwnedBy.Any(user => user.Id == userId))
            .ToListAsync(cancellationToken);
    }
}
