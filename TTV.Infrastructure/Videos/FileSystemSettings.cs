namespace TTV.Infrastructure.Videos
{
    public record FileSystemSettings
    {
        public string VideosPath { get; set; } = string.Empty;
    }
}
