using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;
public class NotificationRepository(DataContext dataContext) : INotificationRepository
{
    private readonly DataContext dataContext = dataContext;

    public void Add(Notification notification)
    {
        dataContext.Add(notification);
    }

    public async Task<Notification?> GetByIdAsync(int notificationId, CancellationToken cancellationToken)
    {
        return await dataContext.Notifications.TagWithCallSite()
            .SingleOrDefaultAsync(notification => notification.Id == notificationId, cancellationToken);
    }
}
