using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> entity)
    {
        var dateConverter = new ValueConverter<DateOnly?, DateTime?>(
            entityValue => entityValue == null ? null : entityValue.Value.ToDateTime(TimeOnly.MinValue),
            sqlValue => sqlValue == null ? null : DateOnly.FromDateTime(sqlValue.Value));

        entity.HasKey(voucher => voucher.Id);
        entity.Property(voucher => voucher.Amount).HasPrecision(18, 4);
        entity.Property(voucher => voucher.ExpirationDate).HasConversion(dateConverter);

        entity.HasOne(voucher => voucher.ClaimedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(voucher => voucher.OrdersAppliedTo).WithOne(map => map.Voucher).OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(voucher => voucher.RemainingAmount);

        entity.ToTable(nameof(DiscountVoucher));
    }
}
