using TTV.Application.Payments;
using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Payments;
public class PayPalPaymentProcessor : IPaymentProcessor
{
    public async Task<Result<Payment>> InitiateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<Result> ConfirmAsync(Payment payment, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

