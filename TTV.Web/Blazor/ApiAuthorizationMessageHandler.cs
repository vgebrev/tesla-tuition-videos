namespace TTV.Web.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IOptionsSnapshot<LocalConfig> options)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { options.Value.ApiRootUri },
            scopes: options.Value.ApiScopes);
    }
}
