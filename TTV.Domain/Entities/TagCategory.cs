namespace TTV.Domain.Entities
{
    public class TagCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
    }
}
