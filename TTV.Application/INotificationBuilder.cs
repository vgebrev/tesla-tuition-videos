using TTV.Domain.Entities;

namespace TTV.Application;
public interface INotificationBuilder
{
    INotificationBuilder For(Order order);
    INotificationBuilder For(User user);
    INotificationBuilder OfType(NotificationType type);
    INotificationBuilder WithData<TData>(TData data);
    Task<Notification> BuildAsync(CancellationToken cancellationToken = default);
}
