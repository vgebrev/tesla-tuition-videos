using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class DocumentApiConsumer(HttpClient httpClient) : IDocumentApiConsumer
{
    private readonly HttpClient httpClient = httpClient;
    private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<DocumentDto[]> GetDocumentsForLessonOwnedByCurrentUserAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"documents/lesson/{lessonId}", cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            return [];
        }
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DocumentDto[]>(responseBody, jsonOptions) ?? [];

    }
}
