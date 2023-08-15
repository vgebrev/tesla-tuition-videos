namespace TTV.Infrastructure.Video
{
    public record FileSystemSettings
    {
        public string VideosPath { get; set; } = string.Empty;
    }
}
