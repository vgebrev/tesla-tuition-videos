namespace TTV.Domain.DomainServices.BackgroundJobs;
public interface IBackgroundJob
{
    Task ExecuteAsync(object? data, CancellationToken cancellationToken = default);
}