namespace TTV.Domain.Entities;
public class LearningPath : BaseEntity
{
    public LearningPath()
    {
        Items = [];
        //Curricula = [];
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public virtual ICollection<LearningPathItem> Items { get; private set; }
    //public virtual ICollection<Curriculum> Curricula { get; private set; }
}
