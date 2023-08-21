using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IUserIdentityService userIdentityService;

        public OrderDataService(IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentityService)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.userIdentityService = userIdentityService;
        }

        public async Task<Order> CreateNewOrderAsync(int[] lessonsIds, CancellationToken cancellationToken = default)
        {
            if (userIdentityService.UserId == null)
            {
                throw new InvalidOperationException("User is not authenticated");
            }

            using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
            await unitOfWork.StartAsync(cancellationToken);

            var user = await unitOfWork.UserRepository.GetByIdAsync(userIdentityService.UserId.Value, cancellationToken) ?? throw new InvalidOperationException("User not found");
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

        public async Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            if (userIdentityService.UserId == null)
            {
                throw new InvalidOperationException("User is not authenticated");
            }

            using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
            var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userIdentityService.UserId.Value, cancellationToken);
            return order;
        }
    }
}
