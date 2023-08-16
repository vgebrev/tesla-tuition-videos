using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TTV.Application.DataServices;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.DataAccess;
using TTV.Infrastructure.Videos;
using TTV.Web.Api;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, shared: true)
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information("Starting TTV.Web.Api");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext());

    builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            policy.WithOrigins(allowedOrigins ?? Array.Empty<string>()).AllowAnyHeader().AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = builder.Configuration["IdentityServer:Authority"];
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudience = builder.Configuration["IdentityServer:Audience"],
                ValidIssuer = builder.Configuration["IdentityServer:Authority"],
                ValidTypes = new[] { "at+jwt" },
            };
        });
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<ILessonDataService, LessonDataService>();
    builder.Services.AddScoped<ITagDataService, TagDataService>();
    builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
    builder.Services.AddScoped<IUserIdentityService, ClaimsIdentityService>();
    builder.Services.AddScoped<IVideoDataService, VideoDataService>();
    builder.Services.AddSingleton<IVideoPathCache, VideoPathCache>();
    builder.Services.AddScoped<IVideoStreamLoader, VideoStreamLoader>();

    builder.Services.Configure<FileSystemSettings>(builder.Configuration.GetSection(nameof(FileSystemSettings)));

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();
    app.UseQueryStringAuthentication();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}