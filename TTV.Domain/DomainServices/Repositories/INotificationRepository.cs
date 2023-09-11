using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;
public interface INotificationRepository
{
    public void Add(Notification notification);
    Task<Notification?> GetByIdAsync(int notificationId, CancellationToken cancellationToken);
}
