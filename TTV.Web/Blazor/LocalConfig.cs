namespace TTV.Web.Blazor;

public record LocalConfig
{
    public string Authority { get; init; } = string.Empty;
    public string ApiRootUri { get; init; } = string.Empty;
    public string ClientId { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string PostLogoutRedirectUri { get; init; } = string.Empty;
    public string ResponseType { get; init; } = string.Empty;
    public string[] ApiScopes { get; init; } = Array.Empty<string>();
}
