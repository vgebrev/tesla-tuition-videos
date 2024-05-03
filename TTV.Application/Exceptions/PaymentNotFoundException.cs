namespace TTV.Application.Exceptions;
public class PaymentNotFoundException : ApplicationException
{
    public PaymentNotFoundException(Guid paymentId)
        : base("Payment not found")
    {
        PaymentId = paymentId;
    }

    public Guid PaymentId { get; }
}
