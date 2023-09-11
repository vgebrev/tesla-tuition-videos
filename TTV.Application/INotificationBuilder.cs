using TTV.Domain.Entities;

namespace TTV.Application;
public interface INotificationBuilder
{
    INotificationBuilder For(Order order);
    INotificationBuilder For(User user);
    INotificationBuilder OfType(NotificationType type);
    Task<Notification> BuildAsync(CancellationToken cancellationToken = default);
}
