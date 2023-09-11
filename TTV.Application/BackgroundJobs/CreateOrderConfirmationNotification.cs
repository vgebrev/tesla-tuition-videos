using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class CreateOrderConfirmationNotification : IBackgroundJob
{
    private readonly ILogger<CreateOrderConfirmationNotification> logger;
    private readonly INotificationManager notificationManager;
    private readonly IBackgroundJobQueue backgroundJob;

    public CreateOrderConfirmationNotification(ILogger<CreateOrderConfirmationNotification> logger, INotificationManager notificationManager, IBackgroundJobQueue backgroundJob)
    {
        this.logger = logger;
        this.notificationManager = notificationManager;
        this.backgroundJob = backgroundJob;
    }
    public async Task ExecuteAsync(object? orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(CreateOrderConfirmationNotification), nameof(ExecuteAsync), orderId);
        var notification = await notificationManager.CreateNotificationAsync(NotificationType.OrderConfirmation, (int)orderId!, cancellationToken);
        backgroundJob.Enqueue<SendNotification>(notification.Id);
    }
}
