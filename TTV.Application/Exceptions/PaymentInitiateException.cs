using TTV.Domain.Entities;

namespace TTV.Application.Exceptions;
public class PaymentInitiateException : ApplicationException
{
    public PaymentInitiateException(int orderId, PaymentType paymentType, string? message)
        : base(message)
    {
        OrderId = orderId;
        PaymentType = paymentType;
    }

    public int OrderId { get; }
    public PaymentType PaymentType { get; }
}
