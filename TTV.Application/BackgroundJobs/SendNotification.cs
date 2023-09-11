using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class SendNotification : IBackgroundJob
{
    private readonly ILogger<SendNotification> logger;
    private readonly INotificationManager notificationManager;

    public SendNotification(ILogger<SendNotification> logger, INotificationManager notificationManager)
    {
        this.logger = logger;
        this.notificationManager = notificationManager;
    }
    public async Task ExecuteAsync(object? notificationId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(SendNotification), nameof(ExecuteAsync), notificationId);
        await notificationManager.SendNotificationAsync((int)notificationId!, cancellationToken);

    }
}
