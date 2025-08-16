using System.Net.Http.Json;
using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public class TagApiConsumer(HttpClient httpClient) : ITagApiConsumer
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<TagDto[]> GetTagsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TagDto[]>("tags", cancellationToken) ?? [];
    }
}
