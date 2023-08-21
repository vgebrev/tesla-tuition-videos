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

        entity.HasMany(lesson => lesson.Videos).WithOne(video => video.Lesson).IsRequired().OnDelete(DeleteBehavior.Restrict);
        entity.OwnsMany(lesson => lesson.Prices, owned =>
        {
            owned.WithOwner(price => price.Lesson);
            owned.HasKey(price => price.Id);
            owned.Property(price => price.Amount).HasPrecision(18, 4);
            owned.Property(price => price.PromoAmount).HasPrecision(18, 4);
            owned.Property(price => price.EffectiveDate).HasConversion(
                entityValue => entityValue.ToDateTime(TimeOnly.MinValue),
                sqlValue => DateOnly.FromDateTime(sqlValue));
            owned.Ignore(price => price.EffectiveAmount);
            owned.ToTable(nameof(Price));
        });
        entity.HasOne(typeof(LookupEntity<LessonType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<LessonType>.Id))
            .HasForeignKey(nameof(Lesson.LessonType)).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(lesson => lesson.Tags).WithMany(tag => tag.Lessons).UsingEntity<Dictionary<string, object>>($"{nameof(Lesson)}{nameof(Tag)}",
            lesson => lesson.HasOne<Tag>().WithMany().HasForeignKey($"{nameof(Tag)}{nameof(Tag.Id)}").OnDelete(DeleteBehavior.Restrict),
            tag => tag.HasOne<Lesson>().WithMany().HasForeignKey($"{nameof(Lesson)}{nameof(Lesson.Id)}").OnDelete(DeleteBehavior.Restrict));

        entity.Ignore(lesson => lesson.CurrentPrice);

        entity.ToTable(nameof(Lesson));
    }
}
