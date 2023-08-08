using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class LookupEntity<TEnum>
    where TEnum : Enum
{
    public TEnum Id { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public LookupEntity() { }
    public LookupEntity(TEnum value)
    {
        this.Id = value;
        this.Name = typeof(TEnum)
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    }
}
