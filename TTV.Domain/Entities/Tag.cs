namespace TTV.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            Lessons = new HashSet<Lesson>();
        }

        public string Name { get; set; } = string.Empty;
        public TagCategory Category { get; set; } = default!;
        public ICollection<Lesson> Lessons { get; set; }
    }
}
