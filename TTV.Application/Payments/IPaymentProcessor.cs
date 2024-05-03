using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application.Payments;

public interface IPaymentProcessor
{
    Task<Result<Payment>> InitiateAsync(Order order, CancellationToken cancellationToken = default);
    Task<Result> ConfirmAsync(Payment payment, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default);
}
