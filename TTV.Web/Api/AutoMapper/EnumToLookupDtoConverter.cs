using AutoMapper;
using TTV.Web.Shared;

namespace TTV.Web.Api.AutoMapper;

public class EnumToLookupDtoConverter<TEnum> : ITypeConverter<TEnum, LookupDto> where TEnum : struct
{
    public LookupDto Convert(TEnum source, LookupDto destination, ResolutionContext context)
    {
        return new LookupDto
        {
            Id = source.GetHashCode(),
            Name = source.GetEnumDescription()
        };
    }
}