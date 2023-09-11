using System.Collections.Concurrent;
using TTV.Domain.DomainServices.BackgroundJobs;

namespace TTV.Infrastructure.BackgroundJobs;
public class BackgroundJobQueue : IBackgroundJobQueue
{
    public record BackgroundJobQueueItem(Type BackgroundJobType, object? Data);

    private readonly ConcurrentQueue<BackgroundJobQueueItem> queue;

    public BackgroundJobQueue()
    {
        queue = new ConcurrentQueue<BackgroundJobQueueItem>();
    }
    
    public void Enqueue<TJob>(object? data) where TJob : IBackgroundJob
    {
        queue.Enqueue(new BackgroundJobQueueItem(typeof(TJob), data));
    }

    public (Type JobType, object? Data)? Dequeue()
    {
        if (queue.TryDequeue(out var item))
        {
            return (item.BackgroundJobType, item.Data);
        }
        return null;
    }
}
