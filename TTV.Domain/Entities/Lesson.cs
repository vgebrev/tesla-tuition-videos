namespace TTV.Domain.Entities;

public class Lesson : BaseEntity
{
    public Lesson()
    {
        Tags = new HashSet<Tag>();
    }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
