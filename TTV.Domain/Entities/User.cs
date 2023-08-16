namespace TTV.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string? Email { get; set; }
    public ICollection<Lesson> OwnedLessons { get; set; } = new HashSet<Lesson>();
}
