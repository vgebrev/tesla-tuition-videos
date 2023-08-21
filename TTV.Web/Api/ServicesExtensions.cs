using Microsoft.EntityFrameworkCore;
using TTV.Application.DataServices;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.DataAccess;
using TTV.Infrastructure.Videos;

namespace TTV.Web.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddTtvServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DataContext")));

        services.AddScoped<ILessonDataService, LessonDataService>();
        services.AddScoped<IOrderDataService, OrderDataService>();
        services.AddScoped<ITagDataService, TagDataService>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IUserIdentityService, ClaimsIdentityService>();
        services.AddScoped<IVideoDataService, VideoDataService>();
        services.AddSingleton<IVideoPathCache, VideoPathCache>();
        services.AddScoped<IVideoStreamLoader, VideoStreamLoader>();

        services.Configure<FileSystemSettings>(configuration.GetSection(nameof(FileSystemSettings)));

        return services;
    }
}
