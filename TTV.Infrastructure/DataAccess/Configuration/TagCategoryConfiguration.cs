using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration
{
    public class TagCategoryConfiguration : IEntityTypeConfiguration<TagCategory>
    {
        public void Configure(EntityTypeBuilder<TagCategory> entity)
        {
            entity.HasKey(tagCategory => tagCategory.Id);
            entity.ToTable(nameof(TagCategory));
        }
    }
}
