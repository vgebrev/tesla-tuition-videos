using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class LearningPathItemConfiguration : IEntityTypeConfiguration<LearningPathItem>
{
    public void Configure(EntityTypeBuilder<LearningPathItem> entity)
    {
        entity.HasKey(learningPathItem => learningPathItem.Id);
        entity.HasOne(learningPathItem => learningPathItem.LearningPath).WithMany(learningPath => learningPath.Items)
            .OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(learningPathItem => learningPathItem.Lesson).WithMany();
        entity.HasOne(learningPathItem => learningPathItem.Parent).WithMany(learningPathItem => learningPathItem.Items);

        var converter = new ValueConverter<ICollection<Curriculum>, string>(
            entityValue => string.Join(",", entityValue.Select(c => c.ToString())),
            dbValue => new HashSet<Curriculum>(dbValue.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(curriculum => Enum.Parse<Curriculum>(curriculum))));
        entity.Property(learningPathItem => learningPathItem.Curricula).HasConversion(converter);

        entity.ToTable(nameof(LearningPathItem));
    }
}
