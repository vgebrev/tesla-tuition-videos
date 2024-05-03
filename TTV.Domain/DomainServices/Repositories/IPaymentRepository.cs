using TTV.Domain.Entities;

namespace TTV.Domain.DomainServices.Repositories;
public interface IPaymentRepository
{
    void Add(Payment payment);
    Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
}
