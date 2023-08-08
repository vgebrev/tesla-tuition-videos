namespace TTV.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<Chapter> Chapters { get; set; } = new HashSet<Chapter>();
}
