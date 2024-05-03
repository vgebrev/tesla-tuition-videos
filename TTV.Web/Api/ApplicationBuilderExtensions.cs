using System.Net;
using System.Web;

namespace TTV.Web.Api;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseQueryStringAuthentication(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[nameof(HttpRequestHeader.Authorization)]))
            {
                var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.Value ?? string.Empty);

                string? token = queryString["access_token"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Request.Headers.Append(nameof(HttpRequestHeader.Authorization), new[] { $"Bearer {token}" });
                }
            }

            await next();
        });
        return app;
    }

    public static IApplicationBuilder UseCustomContentSecurityPolicy(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
         {
             context.Response.Headers.Append("Content-Security-Policy", "default-src 'self' data: gap: https://ssl.gstatic.com 'unsafe-eval'; connect-src 'self' wss:; style-src 'self' data: https://fonts.googleapis.com; font-src 'self' data: https://fonts.gstatic.com; img-src 'self' data: content: https:; media-src *");
             await next();
         });
        return app;
    }
}