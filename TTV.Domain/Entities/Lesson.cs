namespace TTV.Domain.Entities;

public class Lesson : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public byte SequenceNumber { get; set; }
    public virtual Chapter Chapter { get; set; } = new();
}
