using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public interface ITagDataService
{
    Task<IEnumerable<Tag>> GetTagsAsync(CancellationToken cancellationToken = default);
}