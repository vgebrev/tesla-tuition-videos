using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace TTV.Web.Auth;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "ttv_web_api", displayName: "Tesla Tuition Videos API", userClaims: new string[]
            {
                JwtClaimTypes.Email,
                JwtClaimTypes.Subject,
                JwtClaimTypes.Role,
            })
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "ttv_web_blazor",
                ClientName = "Tesla Tuition Videos",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = { "https://localhost:5003/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:5003/authentication/logout-callback" },
                AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "ttv_web_api",
                },
            }
        };
}