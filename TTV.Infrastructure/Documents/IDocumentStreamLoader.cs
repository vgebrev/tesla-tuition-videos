namespace TTV.Infrastructure.Documents;
public interface IDocumentStreamLoader
{
    Task<DocumentStreamInfo> LoadDocumentStreamAsync(int documentId, CancellationToken cancellationToken = default);
}
