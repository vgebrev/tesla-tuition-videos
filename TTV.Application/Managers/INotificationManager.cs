using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface INotificationManager
{
    Task<Notification> CreateNotificationAsync(NotificationType notificationType, int orderId, CancellationToken cancellationToken = default);
    Task<Notification> CreateNotificationAsync(NotificationType notificationType, Guid userId, CancellationToken cancellationToken = default);
    Task SendNotificationAsync(int notificationId, CancellationToken cancellationToken = default);
}
