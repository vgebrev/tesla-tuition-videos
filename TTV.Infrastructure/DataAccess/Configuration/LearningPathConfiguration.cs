using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
{
    public void Configure(EntityTypeBuilder<LearningPath> entity)
    {
        entity.HasKey(learningPath => learningPath.Id);
        entity.HasMany(learningPath => learningPath.Items).WithOne(learningPathItem => learningPathItem.LearningPath).OnDelete(DeleteBehavior.Restrict);

        entity.ToTable(nameof(LearningPath));
    }
}
