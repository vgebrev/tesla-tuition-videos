using Microsoft.EntityFrameworkCore;
using TTV.Domain.DomainServices.Repositories;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Repositories;
public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext dataContext;

    public PaymentRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void Add(Payment payment)
    {
        dataContext.Payments.Add(payment);
    }

    public async Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
    {
        return await dataContext.Payments.TagWithCallSite()
            .Include(payment => payment.PaymentConfirmation)
            .Include(payment => payment.Order).ThenInclude(order => order.Payments)
            .Include(payment => payment.Order).ThenInclude(order => order.PlacedBy)
            .SingleOrDefaultAsync(payment => payment.Id == paymentId, cancellationToken);
    }
}
