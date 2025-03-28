using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
{
    public void Configure(EntityTypeBuilder<LearningPath> entity)
    {
        entity.HasKey(learningPath => learningPath.Id);
        entity.HasMany(learningPath => learningPath.Items).WithOne(learningPathItem => learningPathItem.LearningPath).OnDelete(DeleteBehavior.Restrict);

        var converter = new ValueConverter<ICollection<Curriculum>, string>(
            entityValue => string.Join(",", entityValue.Select(c => c.ToString())),
            dbValue => new HashSet<Curriculum>(dbValue.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(curriculum => Enum.Parse<Curriculum>(curriculum))));

        entity.Property(learningPath => learningPath.Curricula).HasConversion(converter);

        entity.ToTable(nameof(LearningPath));
    }
}
