namespace TTV.Domain.Entities
{
    public class Video : BaseEntity
    {
        public VideoType VideoType { get; set; }
        public string Filename { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public Lesson Lesson { get; set; } = new();
    }
}
