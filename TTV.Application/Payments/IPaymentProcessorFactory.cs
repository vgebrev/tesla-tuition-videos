using TTV.Domain.Entities;

namespace TTV.Application.Payments;
public interface IPaymentProcessorFactory
{
    IPaymentProcessor CreatePaymentProcessor(PaymentType paymentType);
}
