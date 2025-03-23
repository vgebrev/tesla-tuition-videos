using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        //TODO: Curriculum Many-to-many enum relationship?
    }
}
