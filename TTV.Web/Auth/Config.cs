using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace TTV.Web.Auth;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource()
            {
                Name = "role",
                DisplayName ="Your roles and permissions",
                ShowInDiscoveryDocument = true,
                UserClaims =
                [
                    JwtClaimTypes.Role,
                    "permission",
                ],
            }
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(name: "ttv_web_api", displayName: "Tesla Tuition Videos API", userClaims:
            [
                JwtClaimTypes.Email,
                JwtClaimTypes.Subject,
                JwtClaimTypes.Role,
                "permission",
            ])
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "ttv_web_blazor",
                ClientName = "Tesla Tuition Videos",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = { 
                    //"https://localhost:5003/authentication/login-callback"
                    "https://teslatuition.video/authentication/login-callback"
                    },
                PostLogoutRedirectUris = { 
                    //"https://localhost:5003/authentication/logout-callback"
                    "https://teslatuition.video/authentication/logout-callback"
                },
                AllowOfflineAccess = true,
                AllowedScopes =
                [
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "ttv_web_api",
                ],
            }
        ];
}