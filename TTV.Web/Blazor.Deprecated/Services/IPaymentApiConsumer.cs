using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;
public interface IPaymentApiConsumer
{
    Task<ResultDto<PaymentDto?>> InitiateOrderPaymentAsync(PaymentInitiateDto dto, CancellationToken cancellationToken = default);
}