namespace TTV.Domain.Entities;

public class Chapter : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte SequenceNumber { get; set; }
    public virtual Course Course { get; set; } = new();
    public virtual ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
}
