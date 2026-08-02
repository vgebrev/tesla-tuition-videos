using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TTV.DatabaseDeploy;
using TTV.Infrastructure.DataAccess;

using IHost host = Host.CreateDefaultBuilder(args)
    // This host defaults to the Production environment, so CreateDefaultBuilder does not add user
    // secrets itself and they have to be added explicitly. Anything appended here lands at the end
    // of the provider chain and therefore wins, so re-add environment variables and command line
    // afterwards to restore the conventional precedence — otherwise the connection string cannot be
    // overridden for a one-off run against a different database.
    .ConfigureAppConfiguration(configure => configure
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .AddEnvironmentVariables()
        .AddCommandLine(args))
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