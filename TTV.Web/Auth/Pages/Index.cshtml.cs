using System.Reflection;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TTV.Web.Auth.Pages;

[AllowAnonymous]
public class Index(IClientStore clientStore, Config config) : PageModel
{
    private readonly IClientStore clientStore = clientStore;
    private readonly Config config = config;

    public string Version;
    public string ClientUri;
    public string ClientName;
    public async Task OnGetAsync()
    {
        Version = typeof(Duende.IdentityServer.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').First();
        var client = await clientStore.FindClientByIdAsync(config.GetClients().First().ClientId);
        var redirectUri = new Uri(client.RedirectUris.Last());
        ClientUri = new Uri(redirectUri.GetLeftPart(UriPartial.Authority)).ToString();
        ClientName = client.ClientName;

    }
}