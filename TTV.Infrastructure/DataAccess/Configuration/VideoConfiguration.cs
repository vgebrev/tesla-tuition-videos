using Microsoft.EntityFrameworkCore;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;

internal class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Video> entity)
    {
        entity.HasKey(video => video.Id);
        entity.Property(video => video.VideoType).HasConversion<int>().HasColumnName($"{nameof(VideoType)}Id"); ;
        entity.HasOne(typeof(LookupEntity<VideoType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<VideoType>.Id))
            .HasForeignKey(nameof(Video.VideoType)).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(video => video.Lesson).WithMany(lesson => lesson.Videos).HasForeignKey($"{nameof(Video)}Id").OnDelete(DeleteBehavior.Restrict);
        entity.ToTable(nameof(Video));
    }
}
