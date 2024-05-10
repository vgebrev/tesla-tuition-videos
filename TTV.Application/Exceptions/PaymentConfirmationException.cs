namespace TTV.Application.Exceptions;
public class PaymentConfirmationException(Guid paymentId, string? message) : ApplicationException(message)
{
    public Guid PaymentId { get; } = paymentId;
}
