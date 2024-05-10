namespace TTV.Infrastructure.Documents;
public record DocumentStreamInfo
{
    public Stream Stream { get; init; } = Stream.Null;
    public string ContentType { get; init; } = "application/pdf";
    public string Filename { get; init; } = "download.pdf";

    public static DocumentStreamInfo Null { get; } = new() { Stream = Stream.Null, ContentType = string.Empty, Filename = string.Empty };
}
