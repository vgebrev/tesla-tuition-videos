using TTV.Application.Exceptions;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public class OrderManager : IOrderManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IUserIdentityService userIdentity;

    public OrderManager(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentity)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.userIdentity = userIdentity;
    }

    public async Task<Order> CreateNewOrderAsync(int[] lessonsIds, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        try
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken) ?? throw new UserNotFoundException(userId);
            var unownedLessons = await unitOfWork.LessonRepository.GetLessonsNotOwnedByUserAsync(user.Id, lessonsIds, cancellationToken);

            var order = new Order()
            {
                PlacedBy = user,
                PlacedOn = DateTime.Now,
                Status = OrderStatus.New
            };
            order.AddLessons(unownedLessons);

            unitOfWork.OrderRepository.Add(order);
            await unitOfWork.EndAsync(cancellationToken);
            return order;
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetPlacedByUserListAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var orders = await unitOfWork.OrderRepository.GetPlacedByUserListAsync(userId, cancellationToken);
        return orders;

    }

    public async Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userId, cancellationToken);
        return order;
    }

    public async Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await unitOfWork.StartAsync(cancellationToken);
            var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userId, cancellationToken) ?? throw new OrderNotFoundException(orderId);
            var result = order.Complete();
            await unitOfWork.EndAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Result<Order>> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await unitOfWork.StartAsync(cancellationToken);
            var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userId, cancellationToken) ?? throw new OrderNotFoundException(orderId);
            var result = order.Cancel();
            await unitOfWork.EndAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }
}
