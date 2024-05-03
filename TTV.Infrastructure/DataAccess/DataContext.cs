using Microsoft.EntityFrameworkCore;
using TTV.Domain.Entities;
using TTV.Infrastructure.DataAccess.Configuration;

namespace TTV.Infrastructure.DataAccess;

public class DataContext : DbContext
{
    public DataContext() : base() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasEnumLookup<DocumentType>();
        modelBuilder.HasEnumLookup<LessonType>();
        modelBuilder.HasEnumLookup<NotificationType>();
        modelBuilder.HasEnumLookup<OrderStatus>();
        modelBuilder.HasEnumLookup<PaymentStatus>();
        modelBuilder.HasEnumLookup<PaymentType>();
        modelBuilder.HasEnumLookup<VideoType>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<TagCategory> TagCategories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Video> Videos { get; set; }
}
