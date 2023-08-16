namespace TTV.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public User()
    {
        OwnedLessons = new HashSet<Lesson>();
    }
    public string? Email { get; set; }
    public virtual ICollection<Lesson> OwnedLessons { get; set; }
}
