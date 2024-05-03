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

    public IDiscountVoucherRepository DiscountVoucherRepository { get; }
    public ILessonRepository LessonRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IPaymentRepository PaymentRepository { get; }
    public ITagRepository TagRepository { get; }
    public IUserRepository UserRepository { get; set; }
    public IVideoRepository VideoRepository { get; }

    public UnitOfWork(DataContext dataContext)
    {
        this.dataContext = dataContext;
        DiscountVoucherRepository = new DiscountVoucherRepository(dataContext);
        NotificationRepository = new NotificationRepository(dataContext);
        LessonRepository = new LessonRepository(dataContext);
        OrderRepository = new OrderRepository(dataContext);
        PaymentRepository = new PaymentRepository(dataContext);
        TagRepository = new TagRepository(dataContext);
        UserRepository = new UserRepository(dataContext);
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
