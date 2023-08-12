using System.Net.Http.Json;
using System.Text.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class LessonDataService : ILessonDataService
{
    private readonly HttpClient httpClient;

    public LessonDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<LessonDto[]> SearchLessonsAsync(SearchLessonsDto dto)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/lesson/search", dto);
        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LessonDto[]>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? Array.Empty<LessonDto>();
    }
}
