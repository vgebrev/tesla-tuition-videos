using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable(name: "AspNetUsers", schema: "user");
        entity.HasKey(user => user.Id);
        entity.HasMany(user => user.OwnedLessons).WithMany(lesson => lesson.OwnedBy)
            .UsingEntity<Dictionary<string, object>>($"{nameof(User)}{nameof(Lesson)}",
                user => user.HasOne<Lesson>().WithMany().HasForeignKey($"{nameof(Lesson)}{nameof(Lesson.Id)}").OnDelete(DeleteBehavior.Restrict),
                lesson => lesson.HasOne<User>().WithMany().HasForeignKey($"{nameof(User)}{nameof(User.Id)}").OnDelete(DeleteBehavior.Restrict));
    }
}
