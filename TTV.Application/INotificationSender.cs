using TTV.Domain.Entities;

namespace TTV.Application;
public interface INotificationSender
{
    Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
}
