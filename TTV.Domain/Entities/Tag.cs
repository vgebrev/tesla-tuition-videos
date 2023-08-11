namespace TTV.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public TagCategory Category { get; set; } = default!;
    }
}
