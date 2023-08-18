using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly DataContext dataContext;
    private readonly IUserIdentityService userIdentityService;

    public LessonRepository(DataContext dataContext, IUserIdentityService userIdentityService)
    {
        this.dataContext = dataContext;
        this.userIdentityService = userIdentityService;
    }
    public async Task<Lesson?> GetByIdAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Lessons.TagWithCallSite()
            .AsNoTracking()
            .Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Email == userIdentityService.Email))
            .SingleOrDefaultAsync(lesson => lesson.Id == lessonId, cancellationToken);
    }

    public async Task<IEnumerable<Lesson>> GetLessonsOwnedByCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        if (userIdentityService.UserId == null)
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        return await dataContext.Lessons.TagWithCallSite()
            .AsNoTracking()
            .Include(lesson => lesson.Tags).ThenInclude(tag => tag.Category)
            .Include(lesson => lesson.Videos)
            .Include(lesson => lesson.OwnedBy.Where(user => user.Id == userIdentityService.UserId))
            .Where(lesson => lesson.OwnedBy.Any(user => user.Id == userIdentityService.UserId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lesson>> SearchAsync(string? searchText, int[]? searchTagsIds, CancellationToken cancellationToken = default)
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
            .Include(lesson => lesson.OwnedBy.Where(user => user.Email == userIdentityService.Email));

        return await query.ToListAsync(cancellationToken);
    }
}
