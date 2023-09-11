using Microsoft.Extensions.Logging;
using TTV.Application.Exceptions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public class NotificationManager : INotificationManager
{
    private readonly ILogger<NotificationManager> logger;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly INotificationBuilder notificationBuilder;
    private readonly INotificationSender notificationSender;

    public NotificationManager(ILogger<NotificationManager> logger,
                               IUnitOfWorkFactory unitOfWorkFactory,
                               INotificationBuilder notificationBuilder,
                               INotificationSender notificationSender)
    {
        this.logger = logger;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.notificationBuilder = notificationBuilder;
        this.notificationSender = notificationSender;
    }

    public async Task<Notification> CreateNotificationAsync(NotificationType notificationType, int orderId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await unitOfWork.StartAsync(cancellationToken);
            var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, cancellationToken) ?? throw new OrderNotFoundException(orderId);
            var notification = await notificationBuilder
                .For(order)
                .OfType(notificationType)
                .BuildAsync(cancellationToken);
            unitOfWork.NotificationRepository.Add(notification);
            await unitOfWork.EndAsync(cancellationToken);
            return notification;
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }

    public Task<Notification> CreateNotificationAsync(NotificationType notificationType, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SendNotificationAsync(int notificationId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await unitOfWork.StartAsync(cancellationToken);
            var notification = await unitOfWork.NotificationRepository.GetByIdAsync(notificationId, cancellationToken) ?? throw new NotificationNotFoundException(notificationId);
            await notificationSender.SendNotificationAsync(notification, cancellationToken);
            await unitOfWork.EndAsync(cancellationToken);
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }
}
