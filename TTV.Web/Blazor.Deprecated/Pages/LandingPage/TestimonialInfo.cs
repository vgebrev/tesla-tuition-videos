namespace TTV.Web.Blazor.Pages.LandingPage;

public record TestimonialInfo
{
    public byte Seq { get; init; } = 0;
    public string Text { get; init; } = string.Empty;
    public string TestimonialBy { get; init; } = string.Empty;
}
