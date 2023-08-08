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
        modelBuilder.HasDefaultSchema("ttv");
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasEnumLookup<LessonType>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
}
