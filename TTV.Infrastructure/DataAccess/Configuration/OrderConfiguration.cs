using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(order => order.Id);
        entity.Property(order => order.Status).HasConversion<int>().HasColumnName($"{nameof(Order.Status)}Id");
        entity.HasOne(order => order.PlacedBy).WithMany(user => user.Orders).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(order => order.Lessons).WithMany().UsingEntity<Dictionary<string, object>>($"{nameof(Order)}{nameof(Lesson)}",
            order => order.HasOne<Lesson>().WithMany().HasForeignKey($"{nameof(Lesson)}{nameof(Lesson.Id)}").OnDelete(DeleteBehavior.Restrict),
            lesson => lesson.HasOne<Order>().WithMany().HasForeignKey($"{nameof(Order)}{nameof(Order.Id)}").OnDelete(DeleteBehavior.Restrict));

        entity.HasMany(order => order.AppliedVouchers).WithOne(map => map.Order).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(order => order.Notifications).WithOne(n => n.Order).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(typeof(LookupEntity<OrderStatus>)).WithMany()
           .HasPrincipalKey(nameof(LookupEntity<OrderStatus>.Id))
           .HasForeignKey(nameof(Order.Status)).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(order => order.TotalAmount);

        entity.ToTable(nameof(Order));
    }
}
