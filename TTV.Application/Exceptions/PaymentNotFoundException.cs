namespace TTV.Application.Exceptions;
public class PaymentNotFoundException(Guid paymentId) : ApplicationException("Payment not found")
{
    public Guid PaymentId { get; } = paymentId;
}
