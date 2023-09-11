namespace TTV.Domain.Entities;
public class Notification : BaseEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public NotificationType Type { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public bool IsSent { get; set; }
    public DateTime? SentOn { get; set; }
    public int SendAttempts { get; set; }
    public string? LastErrorMessage { get; set; }

    public Order? Order { get; set; }
    public User? User { get; set; }
}
