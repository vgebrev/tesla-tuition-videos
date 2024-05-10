using Microsoft.Extensions.Options;
using TTV.Domain.DomainServices;
using TTV.Application;
using TTV.Domain.Entities;
using TTV.Infrastructure.Notifications.Templates;
using TTV.Infrastructure.Notifications.Templates.MappingExtensions;

namespace TTV.Infrastructure.Notifications;
public class NotificationBuilder(IOptionsSnapshot<SystemSettings> config, ITemplateRenderer templateRenderer) : INotificationBuilder
{
    private readonly EmailSettings emailSettings = config.Value.EmailSettings;
    private readonly ITemplateRenderer templateRenderer = templateRenderer;

    private NotificationType? type;
    private Order? order;
    private User? user;
    private object? data;

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

        if (type == NotificationType.OrderConfirmation || type == NotificationType.OrderComplete || type == NotificationType.OrderCancelled)
        {
            return await BuildOrderNotificationAsync(type.Value, notification, cancellationToken);
        }

        if (type == NotificationType.PasswordReset)
        {
            return await BuildUserNotificationAsync(type.Value, notification, cancellationToken);
        }

        throw new NotSupportedException($"Notification of type {type.Value.ToDisplayString()} not supported");
    }

    private async Task<Notification> BuildUserNotificationAsync(NotificationType type, Notification notification, CancellationToken cancellationToken)
    {
        if (user is null)
        {
            throw new InvalidOperationException("User is not set");
        }

        if (data is null)
        {
            throw new InvalidOperationException("Data is not set");
        }

        notification.To = user.Email ?? throw new InvalidOperationException("Notification recipient email not available");
        notification.Subject = $"{type.ToDisplayString()}";
        notification.Body = type switch
        {
            NotificationType.PasswordReset => await templateRenderer.RenderAsync(new PasswordResetTemplate("Customer", (string)data!), cancellationToken: cancellationToken),
            _ => throw new NotSupportedException($"Notification of type {type.ToDisplayString()} is not a user notification"),
        } ?? throw new InvalidOperationException("Unable to generate notification body from template");
        notification.User = user;
        return notification;
    }

    private async Task<Notification> BuildOrderNotificationAsync(NotificationType type, Notification notification, CancellationToken cancellationToken)
    {
        if (order is null)
        {
            throw new InvalidOperationException("Order is not set");
        }
        notification.To = order.PlacedBy.Email ?? throw new InvalidOperationException("Notification recipient email not available");
        notification.Subject = $"{type.ToDisplayString()} #{order.Id}";
        notification.Body = type switch
        {
            NotificationType.OrderConfirmation => await templateRenderer.RenderAsync(order.ToOrderConfirmationTemplateData(), cancellationToken: cancellationToken),
            NotificationType.OrderComplete => await templateRenderer.RenderAsync(order.ToOrderCompletedTemplateData(), cancellationToken: cancellationToken),
            NotificationType.OrderCancelled => await templateRenderer.RenderAsync(order.ToOrderCancelledTemplateData(), cancellationToken: cancellationToken),
            _ => throw new NotSupportedException($"Notification of type {type.ToDisplayString()} is not an order notification"),
        } ?? throw new InvalidOperationException("Unable to generate notification body from template");
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

    public INotificationBuilder WithData<TData>(TData data)
    {
        this.data = data; 
        return this;
    }
}
