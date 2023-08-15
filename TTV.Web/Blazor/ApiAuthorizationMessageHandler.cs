using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace TTV.Web.Blazor;

public sealed class ApiAuthorizationMessageHandler : DelegatingHandler, IDisposable
{
    private readonly IAccessTokenProvider provider;
    private readonly NavigationManager navigation;
    private readonly AuthenticationStateChangedHandler? authenticationStateChangedHandler;
    
    private AccessToken? lastToken;
    private AuthenticationHeaderValue? cachedHeader;
    private Uri[]? authorizedUris;
    private AccessTokenRequestOptions? tokenOptions;

    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigation,
        IOptionsSnapshot<LocalConfig> options)
    {
        this.provider = provider;
        this.navigation = navigation;

        if (this.provider is AuthenticationStateProvider authStateProvider)
        {
            authenticationStateChangedHandler = _ => { lastToken = null; };
            authStateProvider.AuthenticationStateChanged += authenticationStateChangedHandler;
        }

        ConfigureHandler(
            authorizedUrls: new[] { options.Value.ApiRootUri },
            scopes: options.Value.ApiScopes);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.Now;
        if (authorizedUris == null)
        {
            throw new InvalidOperationException($"The '{nameof(AuthorizationMessageHandler)}' is not configured. " +
                $"Call '{nameof(AuthorizationMessageHandler.ConfigureHandler)}' and provide a list of endpoint urls to attach the token to.");
        }

        if (request.RequestUri != null && authorizedUris.Any(uri => uri.IsBaseOf(request.RequestUri)))
        {
            if (lastToken == null || now >= lastToken.Expires.AddMinutes(-5))
            {
                var tokenResult = tokenOptions != null ?
                    await provider.RequestAccessToken(tokenOptions) :
                    await provider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    lastToken = token;
                    cachedHeader = new AuthenticationHeaderValue("Bearer", lastToken.Value);
                }
            }

            request.Headers.Authorization = cachedHeader;
        }

        return await base.SendAsync(request, cancellationToken);
    }

    public ApiAuthorizationMessageHandler ConfigureHandler(
        IEnumerable<string> authorizedUrls,
        IEnumerable<string>? scopes = null,
        string? returnUrl = null)
    {
        if (authorizedUris != null)
        {
            throw new InvalidOperationException("Handler already configured.");
        }

        if (authorizedUrls == null)
        {
            throw new ArgumentNullException(nameof(authorizedUrls));
        }

        var uris = authorizedUrls.Select(uri => new Uri(uri, UriKind.Absolute)).ToArray();
        if (uris.Length == 0)
        {
            throw new ArgumentException("At least one URL must be configured.", nameof(authorizedUrls));
        }

        authorizedUris = uris;
        var scopesList = scopes?.ToArray();
        if (scopesList != null || returnUrl != null)
        {
            tokenOptions = new AccessTokenRequestOptions
            {
                Scopes = scopesList,
                ReturnUrl = returnUrl
            };
        }

        return this;
    }

    void IDisposable.Dispose()
    {
        if (provider is AuthenticationStateProvider authStateProvider)
        {
            authStateProvider.AuthenticationStateChanged -= authenticationStateChangedHandler;
        }
        Dispose(disposing: true);
    }
}

