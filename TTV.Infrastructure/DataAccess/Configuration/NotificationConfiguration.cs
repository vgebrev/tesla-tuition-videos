using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> entity)
    {
        entity.HasKey(n => n.Id);
        entity.Property(n => n.Type).HasConversion<int>().HasColumnName($"{nameof(Notification.Type)}Id");
        entity.HasOne(typeof(LookupEntity<NotificationType>)).WithMany()
           .HasPrincipalKey(nameof(LookupEntity<NotificationType>.Id))
           .HasForeignKey(nameof(Notification.Type)).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(n => n.Order).WithMany(order => order.Notifications).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(n => n.User).WithMany(user => user.Notifications).OnDelete(DeleteBehavior.Restrict);

        entity.ToTable(nameof(Notification));
    }
}
