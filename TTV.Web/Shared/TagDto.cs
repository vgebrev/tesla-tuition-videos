namespace TTV.Web.Shared
{
    public record TagDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public TagCategoryDto Category { get; init; } = default!;
        public int LessonCount { get; init; }
    }
}
