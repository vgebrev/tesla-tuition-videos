using Microsoft.EntityFrameworkCore.Storage;
using TTV.Domain.DomainServices;
using TTV.Domain.DomainServices.Repositories;
using TTV.Infrastructure.DataAccess.Repositories;

namespace TTV.Infrastructure.DataAccess;

public class UnitOfWork(DataContext dataContext) : IUnitOfWork
{
    private bool disposed = false;
    private IDbContextTransaction? transaction;
    private readonly DataContext dataContext = dataContext;

    public IDiscountVoucherRepository DiscountVoucherRepository { get; } = new DiscountVoucherRepository(dataContext);
    public IDocumentRepository DocumentRepository { get; } = new DocumentRepository(dataContext);
    public ILearningPathRepository LearningPathRepository { get; } = new LearningPathRepository(dataContext);
    public ILessonRepository LessonRepository { get; } = new LessonRepository(dataContext);
    public INotificationRepository NotificationRepository { get; } = new NotificationRepository(dataContext);
    public IOrderRepository OrderRepository { get; } = new OrderRepository(dataContext);
    public IPaymentRepository PaymentRepository { get; } = new PaymentRepository(dataContext);
    public ITagRepository TagRepository { get; } = new TagRepository(dataContext);
    public IUserRepository UserRepository { get; set; } = new UserRepository(dataContext);
    public IVideoRepository VideoRepository { get; } = new VideoRepository(dataContext);

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

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        transaction = await this.dataContext.Database.BeginTransactionAsync(cancellationToken);
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
            await CancelAsync(cancellationToken);
            throw;
        }
    }

    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is not null)
            await transaction.RollbackAsync(cancellationToken);
    }
}
