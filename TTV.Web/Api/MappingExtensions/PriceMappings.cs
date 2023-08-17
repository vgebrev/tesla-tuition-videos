using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class PriceMappings
{
    public static PriceDto ToPriceDto(this Price price)
    {
        return new PriceDto()
        {
            Amount = price.Amount,
            PromoAmount = price.PromoAmount,
        };
    }
}
