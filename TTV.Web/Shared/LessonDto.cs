namespace TTV.Web.Shared
{
    public record LessonDto
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LookupDto LessonType { get; set; } = default!;
    }
}
