using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class CreateOrderNotification(ILogger<CreateOrderNotification> logger, INotificationManager notificationManager, IBackgroundJobQueue backgroundJob) : IBackgroundJob
{
    private readonly ILogger<CreateOrderNotification> logger = logger;
    private readonly INotificationManager notificationManager = notificationManager;
    private readonly IBackgroundJobQueue backgroundJob = backgroundJob;

    public async Task ExecuteAsync(object? data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(CreateOrderNotification), nameof(ExecuteAsync), data);
        var jobData = (JobData)data!;
        var notification = await notificationManager.CreateNotificationAsync(jobData.NotificationType, jobData.OrderId, cancellationToken);
        if (notification != null)
        {
            backgroundJob.Enqueue<SendNotification>(notification.Id);
        }
    }

    public record JobData(int OrderId, NotificationType NotificationType);
}
