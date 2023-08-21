using TTV.Domain.DomainServices.Repositories;

namespace TTV.Domain.DomainServices;

public interface IUnitOfWork : IDisposable
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task EndAsync(CancellationToken cancellationToken = default);
    
    ILessonRepository LessonRepository { get; }
    IOrderRepository OrderRepository { get; }
    ITagRepository TagRepository { get; }
    IUserRepository UserRepository { get; }
    IVideoRepository VideoRepository { get; }

}
