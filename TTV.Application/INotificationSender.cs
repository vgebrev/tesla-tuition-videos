using TTV.Domain.Entities;

namespace TTV.Application;
public interface INotificationSender
{
    Task SendNotificationAsync(Notification notification, bool isTestEnvironment, CancellationToken cancellationToken = default);
}
