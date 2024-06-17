using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TTV.DatabaseDeploy;
using TTV.Infrastructure.DataAccess;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configure => configure.AddUserSecrets(Assembly.GetExecutingAssembly()))
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DataContext>(options => {
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(context.Configuration.GetConnectionString("DataContext"), sql => sql.MigrationsAssembly("TTV.DatabaseDeploy"));
        });
        services.AddScoped<SampleData>();
    }).Build();

await PopulateSampleDataAsync(host.Services);

static async Task PopulateSampleDataAsync(IServiceProvider rootServiceProvider)
{
    using IServiceScope serviceScope = rootServiceProvider.CreateScope();
    IServiceProvider services = serviceScope.ServiceProvider;
    var sampleData = services.GetRequiredService<SampleData>();
    await sampleData.PopulateAsync();
}