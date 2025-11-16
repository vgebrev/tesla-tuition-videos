using Duende.IdentityServer.Models;

namespace TTV.Web.Auth;

public record IdentityServerSettings
{
    public IdentityResourceSettings[] IdentityResources { get; init; } = [];
    public ApiScopeSettings[] ApiScopes { get; init; } = [];
    public ClientSettings[] Clients { get; init; } = [];
    public RoleSettings[] Roles { get; init; } = [];
    public UserSettings[] Users { get; init; } = [];
}

public record IdentityResourceSettings
{
    public string Name { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public bool ShowInDiscoveryDocument { get; init; } = true;
    public string[] UserClaims { get; init; } = [];
    public bool IsBuiltIn { get; init; } = false;
    public string BuiltInType { get; init; } = string.Empty; // "OpenId", "Profile", "Email"
}

public record ApiScopeSettings
{
    public string Name { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string[] UserClaims { get; init; } = [];
}

public record ClientSettings
{
    public string ClientId { get; init; } = string.Empty;
    public string ClientName { get; init; } = string.Empty;
    public bool RequireClientSecret { get; init; } = false;
    public bool RequirePkce { get; init; } = true;
    public string[] AllowedGrantTypes { get; init; } = [];
    public string[] RedirectUris { get; init; } = [];
    public string[] PostLogoutRedirectUris { get; init; } = [];
    public bool AllowOfflineAccess { get; init; } = true;
    public string[] AllowedScopes { get; init; } = [];
}

public record RoleSettings
{
    public string Name { get; init; } = string.Empty;
    public ClaimSettings[] Claims { get; init; } = [];
}

public record ClaimSettings
{
    public string Type { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}

public record UserSettings
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool EmailConfirmed { get; init; } = true;
    public ClaimSettings[] Claims { get; init; } = [];
    public string[] Roles { get; init; } = [];
}