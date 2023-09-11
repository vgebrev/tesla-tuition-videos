namespace TTV.Domain.DomainServices.BackgroundJobs;
public interface IBackgroundJobQueue
{
    void Enqueue<TJob>(object? data) where TJob : IBackgroundJob;

    (Type JobType, object? Data)? Dequeue();
}
