using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class PaymentConfirmationConfiguration : IEntityTypeConfiguration<PaymentConfirmation>
{
    public void Configure(EntityTypeBuilder<PaymentConfirmation> entity)
    {
        entity.HasKey(x => x.Id);
        entity.ToTable(nameof(PaymentConfirmation));
    }
}

internal class PayfastPaymentConfiguration : IEntityTypeConfiguration<PayfastPaymentConfirmation>
{
    public void Configure(EntityTypeBuilder<PayfastPaymentConfirmation> entity)
    {
        entity.Property(x => x.AmountGross).HasPrecision(18, 4);
        entity.Property(x => x.AmountFee).HasPrecision(18, 4);
        entity.Property(x => x.AmountNet).HasPrecision(18, 4);

        entity.HasBaseType<PaymentConfirmation>();
    }
}
