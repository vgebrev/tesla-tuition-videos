using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TTV.Application;
using TTV.Application.BackgroundJobs;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Infrastructure.BackgroundJobs;
using TTV.Infrastructure.DataAccess;
using TTV.Infrastructure.Notifications;
using TTV.Infrastructure.Notifications.Email;
using TTV.Infrastructure.Notifications.Templates;
using TTV.Web.Auth.Data;
using TTV.Web.Auth.Models;

namespace TTV.Web.Auth;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        var connectionString = builder.Configuration.GetConnectionString("IdentityServer");

        builder.Services.AddSingleton<ICorsPolicyService>((container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowedOrigins = builder.Configuration.GetValue<string[]>("Cors:AllowedOrigins")
            };
        });
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.DefaultSchema = "auth_cfg";
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.DefaultSchema = "auth_ops";
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();
        
        builder.Services.AddAuthentication()
            .AddGoogle("Google", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            })
        .AddFacebook("Facebook", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            });

        // TTV Services (for password reset)
        builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));
        
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddScoped<INotificationBuilder, NotificationBuilder>();
        builder.Services.AddScoped<INotificationManager, NotificationManager>();
        builder.Services.AddScoped<INotificationSender, EmailNotificationSender>();
        builder.Services.AddScoped<ITemplateRenderer, TemplateRenderer>();
        builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        // Background jobs
        builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
        builder.Services.AddHostedService<BackgroundJobService>();
        builder.Services.AddScoped<CreatePasswordResetNotification>();
        builder.Services.AddScoped<SendNotification>();

        builder.Services.Configure<SystemSettings>(builder.Configuration.GetSection(nameof(SystemSettings)));

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
        app.UseCors(app => app.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' data: gap: https://ssl.gstatic.com 'unsafe-eval'; connect-src 'self' wss:; style-src 'self' https://fonts.googleapis.com; font-src 'self' data: https://fonts.gstatic.com; img-src 'self' data: content: https:; media-src *; script-src 'self' 'unsafe-eval' 'sha256-orD0/VhH8hLqrLxKHD/HUEMdwqX6/0ve7c5hspX5VJ8=' 'sha256-fa5rxHhZ799izGRP38+h4ud5QXNT0SFaFlh4eqDumBI='");
            await next();
        });
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}