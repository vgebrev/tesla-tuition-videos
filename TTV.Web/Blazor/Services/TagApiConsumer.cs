using System.Net.Http.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class TagApiConsumer : ITagApiConsumer
{
    private readonly HttpClient httpClient;

    public TagApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<TagDto[]> GetTagsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TagDto[]>("api/tag", cancellationToken) ?? Array.Empty<TagDto>();
    }
}
