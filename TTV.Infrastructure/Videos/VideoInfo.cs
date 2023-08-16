namespace TTV.Infrastructure.Videos;

public record VideoInfo
{
    public int Id { get; set; }
    public string Filename { get; set; } = string.Empty;
}
