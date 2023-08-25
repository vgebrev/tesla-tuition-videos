using Microsoft.EntityFrameworkCore;
using TTV.Application;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Infrastructure;
using TTV.Infrastructure.DataAccess;
using TTV.Infrastructure.Videos;

namespace TTV.Web.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddTtvServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DataContext")));

        services.AddScoped<IDiscountVoucherCodeGenerator, DiscountVoucherCodeGenerator>();
        services.AddScoped<IDiscountVoucherManager, DiscountVoucherManager>();
        services.AddScoped<ILessonManager, LessonManager>();
        services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<ITagManager, TagManager>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IUserIdentityService, ClaimsIdentityService>();
        services.AddScoped<IVideoManager, VideoManager>();
        services.AddSingleton<IVideoPathCache, VideoPathCache>();
        services.AddScoped<IVideoStreamLoader, VideoStreamLoader>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
            options.AddPolicy("CanIssueVouchers", policy => policy.RequireClaim("permission", "vouchers.issue"));
        });
        services.Configure<SystemSettings>(configuration.GetSection(nameof(SystemSettings)));

        return services;
    }
}
