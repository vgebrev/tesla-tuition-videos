using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.HasKey(course => course.Id);
        entity.Property(course => course.Id).HasColumnName($"{nameof(Course)}{nameof(Course.Id)}");
        entity.HasMany(course => course.Chapters).WithOne(chapter => chapter.Course).OnDelete(DeleteBehavior.Restrict);
        entity.ToTable(nameof(Course));
    }
}
