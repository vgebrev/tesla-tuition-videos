using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class LookupEntity<TEnum>
    where TEnum : struct, Enum
{
    public TEnum Id { get; set; } = default;
    public string Name { get; set; } = string.Empty;
    public LookupEntity() { }
    public LookupEntity(TEnum value)
    {
        Id = value;
        Name = typeof(TEnum)
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    }
}
