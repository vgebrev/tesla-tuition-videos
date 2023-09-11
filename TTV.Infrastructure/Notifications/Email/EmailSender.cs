using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TTV.Application;

namespace TTV.Infrastructure.Notifications.Email;
public class EmailSender : IEmailSender
{
    private readonly EmailSettings settings;

    public EmailSender(IOptionsSnapshot<SystemSettings> config)
    {
        settings = config.Value.EmailSettings;
    }

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

        var mailMessage = new MailMessage(new MailAddress(settings.DefaultFrom ?? from), new MailAddress(settings.DefaultTo ?? to))
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}
