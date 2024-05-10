using Microsoft.Extensions.Logging;
using TTV.Application.Exceptions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public class NotificationManager(ILogger<NotificationManager> logger,
                           IUnitOfWorkFactory unitOfWorkFactory,
                           INotificationBuilder notificationBuilder,
                           INotificationSender notificationSender) : INotificationManager
{
    private readonly ILogger<NotificationManager> logger = logger;
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;
    private readonly INotificationBuilder notificationBuilder = notificationBuilder;
    private readonly INotificationSender notificationSender = notificationSender;

    public async Task<Notification> CreateNotificationAsync(NotificationType notificationType, int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({NotificationType}, {OrderId})", nameof(CreateNotificationAsync), notificationType, orderId);
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

    public async Task<Notification> CreateNotificationAsync<TData>(NotificationType notificationType, Guid userId, TData data, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await unitOfWork.StartAsync(cancellationToken);
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken) ?? throw new UserNotFoundException(userId);
            var notification = await notificationBuilder
                .For(user)
                .OfType(notificationType)
                .WithData(data)
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
