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
        modelBuilder.HasEnumLookup<LessonType>();
        modelBuilder.HasEnumLookup<VideoType>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<TagCategory> TagCategories { get; set; } = default!;
    public DbSet<Video> Videos { get; set; } = default!;
}
