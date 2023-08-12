using TTV.Web.Shared;

namespace TTV.Application
{
    public interface ITagDataService
    {
        Task<IEnumerable<TagDto>> GetTagsAsync(CancellationToken cancellationToken = default);
    }
}