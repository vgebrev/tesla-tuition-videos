using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface ITagApiConsumer
{
    Task<TagDto[]> GetTagsAsync();
}
