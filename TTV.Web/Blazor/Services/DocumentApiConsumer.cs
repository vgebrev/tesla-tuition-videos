using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class DocumentApiConsumer(HttpClient httpClient) : IDocumentApiConsumer
{
    public async Task<DocumentDto[]> GetDocumentsForLessonOwnedByCurrentUserAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"api/document/lesson/{lessonId}", cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            return [];
        }
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DocumentDto[]>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];

    }
}
