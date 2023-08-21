using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class LessonApiConsumer : ILessonApiConsumer
{
    private readonly HttpClient httpClient;

    public LessonApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<LessonDto?> GetLessonAsync(int lessonId)
    {
        var httpResponse = await httpClient.GetAsync($"api/lesson/{lessonId}");
        if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LessonDto>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? throw new InvalidCastException("Unexpected result");
    }

    public async Task<LessonDto[]> GetLessonsOwnedByCurrentUserAsync()
    {
        var lessons = await httpClient.GetFromJsonAsync<LessonDto[]>("api/lesson/owned") ?? Array.Empty<LessonDto>();
        return lessons;
    }

    public async Task<LessonDto[]> SearchLessonsAsync(LessonSearchDto dto)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/lesson/search", dto);
        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LessonDto[]>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? Array.Empty<LessonDto>();
    }
}
