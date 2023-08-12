using TTV.Web.Shared;

namespace TTV.Web.Blazor.Services;

public interface ITagDataService
{
    Task<TagDto[]> GetTagsAsync();
}
