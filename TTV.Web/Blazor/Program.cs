using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TTV.Web.Blazor;
using TTV.Web.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("TTV Web API", client => client.BaseAddress = new Uri(builder.Configuration["Local:ApiRootUri"] ?? ""))
    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>()
    .AddHttpMessageHandler<AntiforgeryHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TTV Web API"));
builder.Services.AddTransient<ApiAuthorizationMessageHandler>();
builder.Services.AddTransient<AntiforgeryHandler>();
builder.Services.Configure<LocalConfig>(builder.Configuration.GetSection("Local"));
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
    options.UseReduxDevTools();
#endif    
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
    options.AddPolicy("CanIssueVouchers", policy => policy.RequireClaim("permission", "vouchers.issue"));
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IDiscountVoucherApiConsumer, DiscountVoucherApiConsumer>();
builder.Services.AddScoped<IDocumentApiConsumer, DocumentApiConsumer>();
builder.Services.AddScoped<ILessonApiConsumer, LessonApiConsumer>();
builder.Services.AddScoped<IOrderApiConsumer, OrderApiConsumer>();
builder.Services.AddScoped<IPaymentApiConsumer, PaymentApiConsumer>();
builder.Services.AddScoped<ITagApiConsumer, TagApiConsumer>();

await builder.Build().RunAsync();
