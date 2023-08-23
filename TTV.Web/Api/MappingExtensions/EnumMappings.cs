using System.ComponentModel.DataAnnotations;
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

}
