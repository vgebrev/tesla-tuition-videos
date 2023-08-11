using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> entity)
    {
        entity.HasKey(lesson => lesson.Id);
        entity.Property(lesson => lesson.LessonType).HasConversion<int>().HasColumnName($"{nameof(LessonType)}Id");
        entity.HasOne(typeof(LookupEntity<LessonType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<LessonType>.Id))
            .HasForeignKey(nameof(Lesson.LessonType)).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(lesson => lesson.Tags).WithMany().UsingEntity<Dictionary<string, object>>($"{nameof(Lesson)}{nameof(Tag)}",
            l => l.HasOne<Tag>().WithMany().HasForeignKey($"{nameof(Tag)}{nameof(Tag.Id)}").OnDelete(DeleteBehavior.Restrict),
            t => t.HasOne<Lesson>().WithMany().HasForeignKey($"{nameof(Lesson)}{nameof(Lesson.Id)}").OnDelete(DeleteBehavior.Restrict));
        entity.ToTable(nameof(Lesson));
    }
}
