using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class LessonApiConsumer(HttpClient httpClient) : ILessonApiConsumer
{
    private readonly HttpClient httpClient = httpClient;
    private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<LessonDto?> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.GetAsync($"api/lesson/{lessonId}", cancellationToken);
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        
        return JsonSerializer.Deserialize<LessonDto>(responseBody, jsonOptions) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<LessonDto[]> GetLessonsOwnedByCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var lessons = await httpClient.GetFromJsonAsync<LessonDto[]>("api/lesson/own", cancellationToken) ?? [];
        return lessons;
    }

    public async Task<LessonDto[]> SearchLessonsAsync(LessonSearchDto dto, CancellationToken cancellationToken = default)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/lesson/search", dto, cancellationToken);
        var responseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<LessonDto[]>(responseBody, jsonOptions) ?? [];
    }
}
