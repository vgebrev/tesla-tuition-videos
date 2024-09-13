using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain;
using TTV.Domain.Entities;

namespace TTV.Application;
public class AdminOrderComplete(IPaymentManager paymentManager, IOrderManager orderManager) : IAdminOrderComplete
{
    private readonly IPaymentManager paymentManager = paymentManager;
    private readonly IOrderManager orderManager = orderManager;

    public async Task<Result<Order>> CompleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var paymentResult = await paymentManager.InitiateOrderPaymentAsync(orderId, PaymentType.ManualBankTransfer, cancellationToken);
        if (!paymentResult.IsSuccess)
        {
            throw new PaymentInitiateException(orderId, PaymentType.ManualBankTransfer, paymentResult.Message);
        }

        var payment = paymentResult.Value;
        var confirmationResult = await paymentManager.ConfirmPaymentAsync(payment.Id, payment.Type, [], cancellationToken);
        if (!confirmationResult.IsSuccess)
        {
            throw new PaymentConfirmationException(payment.Id, confirmationResult.Message);
        }

        var orderCompleteResult = await orderManager.CompleteOrderAsync(orderId, cancellationToken);
        return orderCompleteResult;
    }
}
