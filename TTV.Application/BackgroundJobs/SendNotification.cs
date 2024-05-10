using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class SendNotification(ILogger<SendNotification> logger, INotificationManager notificationManager) : IBackgroundJob
{
    private readonly ILogger<SendNotification> logger = logger;
    private readonly INotificationManager notificationManager = notificationManager;

    public async Task ExecuteAsync(object? notificationId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(SendNotification), nameof(ExecuteAsync), notificationId);
        await notificationManager.SendNotificationAsync((int)notificationId!, cancellationToken);

    }
}
