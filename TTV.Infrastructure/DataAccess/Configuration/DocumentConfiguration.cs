using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.DataAccess.Configuration;
internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> entity)
    {
        entity.HasKey(document => document.Id);
        entity.Property(document => document.DocumentType).HasConversion<int>().HasColumnName($"{nameof(DocumentType)}Id");

        entity.HasOne(typeof(LookupEntity<DocumentType>)).WithMany()
            .HasPrincipalKey(nameof(LookupEntity<DocumentType>.Id))
            .HasForeignKey(nameof(Document.DocumentType)).OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(document => document.Lessons).WithMany(lesson => lesson.Documents).UsingEntity<Dictionary<string, object>>($"{nameof(Lesson)}{nameof(Document)}",
            document => document.HasOne<Lesson>().WithMany().HasForeignKey($"{nameof(Lesson)}{nameof(Lesson.Id)}").OnDelete(DeleteBehavior.Restrict),
            lesson => lesson.HasOne<Document>().WithMany().HasForeignKey($"{nameof(Document)}{nameof(Document.Id)}").OnDelete(DeleteBehavior.Restrict));

        entity.ToTable(nameof(Document));
    }
}
