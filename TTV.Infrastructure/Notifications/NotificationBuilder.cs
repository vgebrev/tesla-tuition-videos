using Microsoft.Extensions.Options;
using TTV.Domain.DomainServices;
using TTV.Application;
using TTV.Domain.Entities;
using TTV.Infrastructure.Notifications.Templates;
using TTV.Infrastructure.Notifications.Templates.MappingExtensions;

namespace TTV.Infrastructure.Notifications;
public class NotificationBuilder : INotificationBuilder
{
    private readonly EmailSettings emailSettings;
    private readonly ITemplateRenderer templateRenderer;

    private NotificationType? type;
    private Order? order;
    private User? user;

    public NotificationBuilder(IOptionsSnapshot<SystemSettings> config, ITemplateRenderer templateRenderer)
    {
        emailSettings = config.Value.EmailSettings;
        this.templateRenderer = templateRenderer;
    }

    public async Task<Notification> BuildAsync(CancellationToken cancellationToken = default)
    {
        if (type is null)
        {
            throw new InvalidOperationException("Notification type must be specified");
        }

        var notification = new Notification()
        {
            From = emailSettings.DefaultFrom,
            CreatedOn = DateTime.Now,
            Type = type.Value,
        };

        if (type == NotificationType.OrderConfirmation)
        {
            return await BuildOrderNotificationAsync(type.Value, notification, cancellationToken);
        }

        throw new NotSupportedException($"Notification of type {type.Value.ToDisplayString()} not supported");
    }

    private async Task<Notification> BuildOrderNotificationAsync(NotificationType type, Notification notification, CancellationToken cancellationToken)
    {
        if (order is null)
        {
            throw new InvalidOperationException("Order is not set");
        }
        notification.To = order.PlacedBy.Email ?? throw new InvalidOperationException("Notification recipient email not available");
        notification.Subject = $"{type.ToDisplayString()} #{order.Id}";
        notification.Body = await templateRenderer.RenderAsync(order.ToTemplateData(), cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Unable to generate notification body from template");
        notification.Order = order;
        return notification;
    }

    public INotificationBuilder For(Order order)
    {
        this.order = order;
        return this;
    }

    public INotificationBuilder For(User user)
    {
        this.user = user;
        return this;
    }

    public INotificationBuilder OfType(NotificationType type)
    {
        this.type = type;
        return this;
    }
}
