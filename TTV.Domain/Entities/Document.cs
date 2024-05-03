namespace TTV.Domain.Entities;
public class Document : BaseEntity
{
    public Document()
    {
        Lessons = new HashSet<Lesson>();
    }
    public DocumentType DocumentType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Filename { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public virtual ICollection<Lesson> Lessons { get; set; }
}
