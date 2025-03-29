using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class EnumMappings
{
    public static LookupDto ToLookupDto<TEnum>(this TEnum value) where TEnum : Enum =>
        new()
        {
            Id = value.GetHashCode(),
            Name = typeof(TEnum)
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString()
        };

    public static IEnumerable<LookupDto> ToLookupDtoEnumerable<TEnum>(this IEnumerable<TEnum> values) where TEnum : Enum =>
        values.Select(ToLookupDto);
}
