using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class OrderDiscountVoucherConfiguration : IEntityTypeConfiguration<OrderDiscountVoucher>
{
    public void Configure(EntityTypeBuilder<OrderDiscountVoucher> entity)
    {
        entity.HasKey(map => new { map.OrderId, map.VoucherId });

        entity.Property(map => map.Amount).HasPrecision(18, 4);

        entity.HasOne(map => map.Order)
            .WithMany(order => order.AppliedVouchers)
            .HasForeignKey(map => map.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(map => map.Voucher)
            .WithMany(voucher => voucher.OrdersAppliedTo)
            .HasForeignKey(map => map.VoucherId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ToTable(nameof(OrderDiscountVoucher));
    }
}
