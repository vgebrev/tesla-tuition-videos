using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class CreateOrderNotification : IBackgroundJob
{
    private readonly ILogger<CreateOrderNotification> logger;
    private readonly INotificationManager notificationManager;
    private readonly IBackgroundJobQueue backgroundJob;

    public CreateOrderNotification(ILogger<CreateOrderNotification> logger, INotificationManager notificationManager, IBackgroundJobQueue backgroundJob)
    {
        this.logger = logger;
        this.notificationManager = notificationManager;
        this.backgroundJob = backgroundJob;
    }
    public async Task ExecuteAsync(object? data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(CreateOrderNotification), nameof(ExecuteAsync), data);
        var jobData = (JobData)data!;
        var notification = await notificationManager.CreateNotificationAsync(jobData.NotificationType, jobData.OrderId, cancellationToken);
        backgroundJob.Enqueue<SendNotification>(notification.Id);
    }

    public record JobData(int OrderId, NotificationType NotificationType);
}
