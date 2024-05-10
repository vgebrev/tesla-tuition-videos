namespace TTV.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            Lessons = [];
        }

        public string Name { get; set; } = string.Empty;
        public TagCategory Category { get; set; } = new();
        public ICollection<Lesson> Lessons { get; set; }
    }
}
