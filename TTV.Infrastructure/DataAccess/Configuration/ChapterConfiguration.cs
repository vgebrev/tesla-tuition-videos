using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> entity)
    {
        entity.HasKey(chapter => chapter.Id);
        entity.Property(chapter => chapter.Id).HasColumnName($"{nameof(Chapter)}{nameof(Chapter.Id)}");
        entity.HasOne(chapter => chapter.Course).WithMany(course => course.Chapters).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(chapter => chapter.Lessons).WithOne(lesson => lesson.Chapter).OnDelete(DeleteBehavior.Restrict);
        entity.ToTable(nameof(Chapter));
    }
}
