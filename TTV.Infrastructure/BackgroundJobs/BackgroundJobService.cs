using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTV.Domain.DomainServices.BackgroundJobs;

namespace TTV.Infrastructure.BackgroundJobs;
public class BackgroundJobService : BackgroundService
{
    private readonly ILogger<BackgroundJobService> logger;
    private readonly IBackgroundJobQueue jobQueue;
    private readonly IServiceProvider services;

    public BackgroundJobService(ILogger<BackgroundJobService> logger, IBackgroundJobQueue jobQueue, IServiceProvider services)
    {
        this.logger = logger;
        this.jobQueue = jobQueue;
        this.services = services;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("{Service} is executing", nameof(BackgroundJobService));
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var jobItem = jobQueue.Dequeue();
                if (jobItem is null)
                {
                    continue;
                }

                await ExecuteJobAsync(jobItem.Value.JobType, jobItem.Value.Data, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing background job");
            }
        }
    }

    private async Task ExecuteJobAsync(Type jobType, object? data, CancellationToken stoppingToken)
    {
        using var scope = services.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService(jobType) as IBackgroundJob;
        if (job is not null)
        {
            await job.ExecuteAsync(data, stoppingToken);
        }
    }
}
