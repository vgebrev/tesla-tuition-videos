namespace TTV.Application.Exceptions;
internal class NotificationNotFoundException(int notificationId) : ApplicationException("Notification not found")
{
    public int NotificationId { get; } = notificationId;
}
