using Microsoft.Extensions.Options;
using TTV.Application;
using TTV.Application.Payments;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Payments;
public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly SystemSettings settings;

    public PaymentProcessorFactory(IOptionsSnapshot<SystemSettings> config, IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
        settings = config.Value;
    }
    public IPaymentProcessor CreatePaymentProcessor(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.ManualBankTransfer => new ManualBankTransferPaymentProcessor(),
            PaymentType.Payfast => new PayfastPaymentProcessor(settings.PayfastSettings, httpClientFactory),
            PaymentType.PayPal => new PayPalPaymentProcessor(),
            _ => throw new NotImplementedException(),
        }; ;
    }
}
