using TTV.Domain.Entities;

namespace TTV.Application.Exceptions;
public class PaymentInitiateException(int orderId, PaymentType paymentType, string? message) : ApplicationException(message)
{
    public int OrderId { get; } = orderId;
    public PaymentType PaymentType { get; } = paymentType;
}
