using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.HasKey(payment => payment.Id);
        entity.HasIndex(payment => payment.ExternalIdentifier).IsUnique();
        entity.Property(payment => payment.Amount).HasPrecision(18, 4);
        entity.Property(payment => payment.Type).HasConversion<int>().HasColumnName($"{nameof(Payment.Type)}Id");
        entity.HasOne(typeof(LookupEntity<PaymentType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<PaymentType>.Id))
            .HasForeignKey(nameof(Payment.Type)).OnDelete(DeleteBehavior.Restrict);

        entity.Property(payment => payment.Status).HasConversion<int>().HasColumnName($"{nameof(Payment.Status)}Id");
        entity.HasOne(typeof(LookupEntity<PaymentStatus>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<PaymentStatus>.Id))
            .HasForeignKey(nameof(Payment.Status)).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(payment => payment.CreatedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(payment => payment.ProcessedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(payment => payment.Order).WithMany(order => order.Payments).OnDelete(DeleteBehavior.Restrict);


        entity.HasOne(x => x.PaymentConfirmation)
            .WithOne(p => p.Payment)
            .HasForeignKey<PaymentConfirmation>(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ToTable(nameof(Payment));
    }
}
