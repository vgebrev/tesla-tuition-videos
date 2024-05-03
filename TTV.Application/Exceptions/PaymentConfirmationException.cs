namespace TTV.Application.Exceptions;
public class PaymentConfirmationException : ApplicationException
{
    public PaymentConfirmationException(Guid paymentId, string? message)
        : base(message)
    {
        PaymentId = paymentId;
    }

    public Guid PaymentId { get; }
}
