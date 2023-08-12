using System.Net.Http.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class TagDataService : ITagDataService
{
    private readonly HttpClient httpClient;

    public TagDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<TagDto[]> GetTagsAsync()
    {
        return await httpClient.GetFromJsonAsync<TagDto[]>("api/tag") ?? Array.Empty<TagDto>();
    }
}
