using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTV.Application;
using TTV.Application.Payments;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Payments;
public class PaymentProcessorFactory(ILoggerFactory loggerFactory, IOptionsSnapshot<SystemSettings> config, IHttpClientFactory httpClientFactory) : IPaymentProcessorFactory
{
    private readonly ILoggerFactory loggerFactory = loggerFactory;
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;
    private readonly SystemSettings settings = config.Value;

    public IPaymentProcessor CreatePaymentProcessor(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.ManualBankTransfer => new ManualBankTransferPaymentProcessor(),
            PaymentType.Payfast => new PayfastPaymentProcessor(loggerFactory.CreateLogger<PayfastPaymentProcessor>(), settings.PayfastSettings, httpClientFactory),
            PaymentType.PayPal => new PayPalPaymentProcessor(),
            _ => throw new NotImplementedException(),
        }; ;
    }
}
