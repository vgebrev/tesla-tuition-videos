using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface INotificationManager
{
    Task<Notification?> CreateNotificationAsync(NotificationType notificationType, int orderId, CancellationToken cancellationToken = default);
    Task<Notification?> CreateNotificationAsync<TData>(NotificationType notificationType, Guid userId, TData data, CancellationToken cancellationToken = default);
    Task SendNotificationAsync(int notificationId, CancellationToken cancellationToken = default);
}
