namespace TTV.Domain.Entities;
public class LearningPath(IEnumerable<Curriculum> curricula) : BaseEntity
{
    public LearningPath() : this([])
    {}

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public virtual ICollection<LearningPathItem> Items { get; private set; } = [];
    public virtual ICollection<Curriculum> Curricula { get; private set; } = new HashSet<Curriculum>(curricula);
}
