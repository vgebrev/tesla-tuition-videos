namespace TTV.Domain.Entities;

public class LearningPathItem : BaseEntity
{
    public LearningPathItem()
    {
        Items = [];
        //Curricula = [];
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Sequence { get; set; }
    public LearningPathItem? Parent { get; set; }
    public LearningPath LearningPath { get; set; } = new();
    public Lesson? Lesson { get; set; }
    public virtual ICollection<LearningPathItem> Items { get; private set; }
    //public virtual ICollection<Curriculum> Curricula { get; private set; }
}
