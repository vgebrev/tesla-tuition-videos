using Microsoft.EntityFrameworkCore;
using TTV.Application;
using TTV.Application.BackgroundJobs;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Infrastructure;
using TTV.Infrastructure.BackgroundJobs;
using TTV.Infrastructure.DataAccess;
using TTV.Infrastructure.Notifications;
using TTV.Infrastructure.Notifications.Email;
using TTV.Infrastructure.Notifications.Templates;
using TTV.Infrastructure.Videos;

namespace TTV.Web.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddTtvServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DataContext")));
        
        services.AddScoped<IDiscountVoucherCodeGenerator, DiscountVoucherCodeGenerator>();
        services.AddScoped<IDiscountVoucherManager, DiscountVoucherManager>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<ILessonManager, LessonManager>();
        services.AddScoped<INotificationBuilder, NotificationBuilder>();
        services.AddScoped<INotificationManager, NotificationManager>();
        services.AddScoped<INotificationSender, EmailNotificationSender>();
        services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<ITagManager, TagManager>();
        services.AddScoped<ITemplateRenderer, TemplateRenderer>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IUserIdentityService, ClaimsIdentityService>();
        services.AddScoped<IVideoManager, VideoManager>();
        services.AddSingleton<IVideoPathCache, VideoPathCache>();
        services.AddScoped<IVideoStreamLoader, VideoStreamLoader>();

        // Background jobs
        services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
        services.AddHostedService<BackgroundJobService>();
        services.AddScoped<CreateOrderConfirmationNotification>();
        services.AddScoped<SendNotification>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
            options.AddPolicy("CanIssueVouchers", policy => policy.RequireClaim("permission", "vouchers.issue"));
        });
        services.Configure<SystemSettings>(configuration.GetSection(nameof(SystemSettings)));

        return services;
    }
}
