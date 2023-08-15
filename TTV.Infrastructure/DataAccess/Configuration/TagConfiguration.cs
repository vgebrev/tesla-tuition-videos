using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> entity)
    {
        entity.HasKey(tag => tag.Id);
        entity.HasOne(tag => tag.Category).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.ToTable(nameof(Tag));
    }
}
