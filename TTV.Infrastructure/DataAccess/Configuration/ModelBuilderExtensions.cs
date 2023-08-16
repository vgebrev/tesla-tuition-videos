using Microsoft.EntityFrameworkCore;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal static class ModelBuilderExtensions
{
    public static void HasEnumLookup<TEnum>(this ModelBuilder modelBuilder, string? tableName = null)
        where TEnum : struct, Enum
    {
        var lookupEntityType = typeof(LookupEntity<>).MakeGenericType(typeof(TEnum));
        var lookupBuilder = modelBuilder.Entity(lookupEntityType);
        var idProperty = nameof(LookupEntity<TEnum>.Id);
        lookupBuilder.HasKey(idProperty);
        lookupBuilder.Property(idProperty).ValueGeneratedNever();
        lookupBuilder.ToTable(tableName ?? typeof(TEnum).Name);

        LookupEntity<TEnum>[] data = Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
            .Select(v => Activator.CreateInstance(lookupEntityType, v)).Cast<LookupEntity<TEnum>>()
            .ToArray();
        lookupBuilder.HasData(data);
    }
}
