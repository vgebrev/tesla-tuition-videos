using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public interface ITagManager
{
    Task<IEnumerable<Tag>> GetTagsAsync(CancellationToken cancellationToken = default);
}