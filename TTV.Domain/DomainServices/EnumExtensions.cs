using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TTV.Domain.DomainServices;

public static class EnumExtensions
{
    public static string ToDisplayString<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        typeof(TEnum)
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
}
