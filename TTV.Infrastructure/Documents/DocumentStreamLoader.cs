using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTV.Application;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Infrastructure.Documents;
public class DocumentStreamLoader(ILogger<DocumentStreamLoader> logger, IDocumentManager documentManager, IOptionsSnapshot<SystemSettings> config) : IDocumentStreamLoader
{
    private readonly SystemSettings settings = config.Value;

    public async Task<DocumentStreamInfo> LoadDocumentStreamAsync(int documentId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{MethodName}({DocumentId})", nameof(LoadDocumentStreamAsync), documentId);
        var document = await documentManager.GetDocumentByIdAsync(documentId, cancellationToken);
        if (document == null)
        {
            return DocumentStreamInfo.Null;
        }
        var path = Path.Combine(settings.DocumentsPath, document.RelativePath, document.Filename);
        var stream = File.OpenRead(path);
        return new DocumentStreamInfo
        {
            Stream = stream,
            ContentType = GetContentType(document.DocumentType),
            Filename = document.Filename
        };
    }

    private static string GetContentType(DocumentType documentType) => documentType switch
    {
        DocumentType.ExercisePdf => "application/pdf",
        DocumentType.Reference => "application/pdf",
        _ => throw new ApplicationException($"Unknown content type for document type '{documentType.ToDisplayString()}'")
    };
}
