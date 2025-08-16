using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.Extensions.Options;

namespace TTV.Web.Auth;

public class Config(IOptionsSnapshot<IdentityServerSettings> config)
{
    private readonly IdentityServerSettings settings = config.Value;

    public IEnumerable<IdentityResource> GetIdentityResources()
    {
        var resources = new List<IdentityResource>();

        foreach (var resource in settings.IdentityResources)
        {
            if (resource.IsBuiltIn)
            {
                resources.Add(resource.BuiltInType switch
                {
                    "OpenId" => new IdentityResources.OpenId(),
                    "Profile" => new IdentityResources.Profile(),
                    "Email" => new IdentityResources.Email(),
                    _ => throw new InvalidOperationException($"Unknown built-in identity resource type: {resource.BuiltInType}")
                });
            }
            else
            {
                resources.Add(new IdentityResource
                {
                    Name = resource.Name,
                    DisplayName = resource.DisplayName,
                    ShowInDiscoveryDocument = resource.ShowInDiscoveryDocument,
                    UserClaims = resource.UserClaims.ToList()
                });
            }
        }

        return resources;
    }

    public IEnumerable<ApiScope> GetApiScopes()
    {
        return settings.ApiScopes.Select(scope => new ApiScope(
            name: scope.Name,
            displayName: scope.DisplayName,
            userClaims: scope.UserClaims
        ));
    }

    public IEnumerable<Client> GetClients()
    {
        return settings.Clients.Select(client => new Client
        {
            ClientId = client.ClientId,
            ClientName = client.ClientName,
            RequireClientSecret = client.RequireClientSecret,
            AllowedGrantTypes = client.AllowedGrantTypes.ToList(),
            RedirectUris = client.RedirectUris.ToList(),
            PostLogoutRedirectUris = client.PostLogoutRedirectUris.ToList(),
            AllowOfflineAccess = client.AllowOfflineAccess,
            AllowedScopes = client.AllowedScopes.ToList()
        });
    }

    public IEnumerable<RoleSettings> GetRoles()
    {
        return settings.Roles;
    }

    public IEnumerable<UserSettings> GetUsers()
    {
        return settings.Users;
    }
}