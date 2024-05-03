using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public interface IPaymentManager
{
    Task<Result<Payment>> InitiateOrderPaymentAsync(int orderId, PaymentType paymentType, CancellationToken cancellationToken = default);

    Task<Result> ConfirmPaymentAsync(Guid paymentId, PaymentType paymentType, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default);
}