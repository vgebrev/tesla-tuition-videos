using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class PaymentMappings
{
    public static PaymentDto? ToPaymentDto(this Payment? payment)
    {
        if (payment == null)
        {
            return null;
        }

        return new()
        {
            Id = payment.Id,
            CreatedOn = payment.CreatedOn,
            ExternalIdentifier = payment.ExternalIdentifier,
            PaymentMethod = (PaymentMethod)(int)payment.Type,
            Amount = payment.Amount,
            Status = payment.Status.ToLookupDto()
        };
    }

    public static IEnumerable<PaymentDto> ToEnumerablePaymentDto(this IEnumerable<Payment> payments) =>
        payments.Select(p => p.ToPaymentDto()!);
}
