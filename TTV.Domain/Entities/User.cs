namespace TTV.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public User()
    {
        Orders = new HashSet<Order>();
        OwnedLessons = new HashSet<Lesson>();
    }
    public string? Email { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Lesson> OwnedLessons { get; set; }
}
