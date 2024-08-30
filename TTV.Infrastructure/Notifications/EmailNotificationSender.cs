using Microsoft.Extensions.Logging;
using TTV.Application;
using TTV.Domain.Entities;
using TTV.Infrastructure.Notifications.Email;

namespace TTV.Infrastructure.Notifications;
public class EmailNotificationSender(ILogger<EmailNotificationSender> logger, IEmailSender emailSender) : INotificationSender
{
    private readonly ILogger<EmailNotificationSender> logger = logger;
    private readonly IEmailSender emailSender = emailSender;

    public async Task SendNotificationAsync(Notification notification, bool isTestEnvironment, CancellationToken cancellationToken = default)
    {
        try
        {
            await emailSender.SendEmailAsync(notification.To, notification.From, (isTestEnvironment ? "[Test] " : "") + notification.Subject, notification.Body, cancellationToken);
            notification.SentOn = DateTime.Now;
            notification.IsSent = true;
        }
        catch (Exception ex)
        {
            notification.LastErrorMessage = ex.ToString();
            logger.LogError(ex, "Error sending notification {Id}", notification.Id);
        }
        finally
        {
            notification.SendAttempts++;
        }
    }
}
