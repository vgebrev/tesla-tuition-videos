using Microsoft.Extensions.Logging;
using TTV.Application.Managers;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Domain.Entities;

namespace TTV.Application.BackgroundJobs;
public class CreatePasswordResetNotification : IBackgroundJob
{
    private readonly ILogger<CreateOrderNotification> logger;
    private readonly INotificationManager notificationManager;
    private readonly IBackgroundJobQueue backgroundJob;

    public CreatePasswordResetNotification(ILogger<CreateOrderNotification> logger, INotificationManager notificationManager, IBackgroundJobQueue backgroundJob)
    {
        this.logger = logger;
        this.notificationManager = notificationManager;
        this.backgroundJob = backgroundJob;
    }
    public async Task ExecuteAsync(object? data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{JobName}.{MethodName}({@Data})", nameof(CreateOrderNotification), nameof(ExecuteAsync), data);
        var jobData = (JobData)data!;
        var notification = await notificationManager.CreateNotificationAsync(NotificationType.PasswordReset, jobData.UserId, jobData.PasswordResetLink, cancellationToken);
        backgroundJob.Enqueue<SendNotification>(notification.Id);
    }

    public record JobData(Guid UserId, string PasswordResetLink);
}
