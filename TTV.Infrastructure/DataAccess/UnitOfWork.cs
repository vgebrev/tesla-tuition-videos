using Microsoft.EntityFrameworkCore.Storage;
using TTV.Domain.DomainServices;
using TTV.Domain.DomainServices.Repositories;
using TTV.Infrastructure.DataAccess.Repositories;

namespace TTV.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private bool disposed = false;
    private IDbContextTransaction? transaction;
    private readonly DataContext dataContext;

    public ILessonRepository LessonRepository { get; }
    public ITagRepository TagRepository { get; }
    public IVideoRepository VideoRepository { get; }

    public UnitOfWork(DataContext dataContext, IUserIdentityService userIdentityService)
    {
        this.dataContext = dataContext;
        TagRepository = new TagRepository(dataContext);
        LessonRepository = new LessonRepository(dataContext, userIdentityService);
        VideoRepository = new VideoRepository(dataContext);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                dataContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task EndAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dataContext.SaveChangesAsync(cancellationToken);
            if (transaction is not null)
                await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction is not null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        transaction = await this.dataContext.Database.BeginTransactionAsync(cancellationToken);
    }
}
