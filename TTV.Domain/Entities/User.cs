namespace TTV.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public User()
    {
        Orders = new HashSet<Order>();
        OwnedLessons = new HashSet<Lesson>();
        Notifications = new HashSet<Notification>();
    }
    public string? Email { get; set; }
    public virtual ICollection<Order> Orders { get; private set; }
    public virtual ICollection<Lesson> OwnedLessons { get; private set; }
    public virtual ICollection<Notification> Notifications { get; private set; }
}
