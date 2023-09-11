namespace TTV.Application.Exceptions;
internal class NotificationNotFoundException : ApplicationException
{
    public NotificationNotFoundException(int notificationId)
        : base("Notification not found")
    {
        NotificationId = notificationId;
    }

    public int NotificationId { get; }
}
