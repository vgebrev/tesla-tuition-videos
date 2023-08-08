using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TTV.Web.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("TTV Web API", client => client.BaseAddress = new Uri(builder.Configuration["Local:ApiRootUri"] ?? ""))
    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>()
    .AddHttpMessageHandler<AntiforgeryHandler>();
    
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TTV Web API"));
builder.Services.AddTransient<AntiforgeryHandler>();
builder.Services.AddTransient<ApiAuthorizationMessageHandler>();
builder.Services.Configure<LocalConfig>(builder.Configuration.GetSection("Local"));
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
