using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TTV.Application;

namespace TTV.Infrastructure.Notifications.Email;
public class EmailSender(IOptionsSnapshot<SystemSettings> config) : IEmailSender
{
    private readonly EmailSettings settings = config.Value.EmailSettings;

    public async Task SendEmailAsync(string to, string from, string subject, string body, CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient(settings.SmtpHost)
        {
            EnableSsl = settings.EnableSsl ?? false,
            Port = settings.SmtpPort ?? (settings.EnableSsl ?? false ? 587 : 25),
        };

        if (settings.SmtpUsername is not null && settings.SmtpPassword is not null)
        {
            smtpClient.Credentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);
        }

        using var mailMessage = new MailMessage(new MailAddress(settings.DefaultFrom ?? from, "Tesla Tuition Videos"), new MailAddress(settings.DefaultTo ?? to))
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        smtpClient.Send(mailMessage);
        await Task.CompletedTask;
    }
}
