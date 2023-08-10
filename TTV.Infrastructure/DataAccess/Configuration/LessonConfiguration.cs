using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> entity)
    {
        entity.HasKey(lesson => lesson.Id);
        entity.Property(lesson => lesson.Id).HasColumnName($"{nameof(Lesson)}{nameof(Lesson.Id)}");
        entity.Property(lesson => lesson.LessonType).HasConversion<int>().HasColumnName($"{nameof(LessonType)}Id");
        entity.HasOne(typeof(LookupEntity<LessonType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<LessonType>.Id))
            .HasForeignKey(nameof(Lesson.LessonType)).OnDelete(DeleteBehavior.Restrict);
        entity.ToTable(nameof(Lesson));
    }
}
