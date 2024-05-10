using TTV.Domain.DomainServices.Repositories;

namespace TTV.Domain.DomainServices;

public interface IUnitOfWork : IDisposable
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task EndAsync(CancellationToken cancellationToken = default);
    Task CancelAsync(CancellationToken cancellationToken = default);

    IDiscountVoucherRepository DiscountVoucherRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    ILessonRepository LessonRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IOrderRepository OrderRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    ITagRepository TagRepository { get; }
    IUserRepository UserRepository { get; }
    IVideoRepository VideoRepository { get; }
}
