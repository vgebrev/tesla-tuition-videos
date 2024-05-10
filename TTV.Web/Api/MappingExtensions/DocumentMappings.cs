using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class DocumentMappings
{
    public static DocumentDto ToDocumentDto(this Document document) =>
        new()
        {
            Id = document.Id,
            Title = document.Title,
            DocumentType = document.DocumentType.ToLookupDto()
        };

    public static IEnumerable<DocumentDto> ToDocumentDtoEnumerable(this IEnumerable<Document> documents) =>
        documents.Select(ToDocumentDto);
}
