using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class EnumMappings
{
    public static LookupDto ToLookupDto<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        return new LookupDto
        {
            Id = value.GetHashCode(),
            Name = typeof(TEnum)
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString()
        };
    }
}
