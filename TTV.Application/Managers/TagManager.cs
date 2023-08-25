using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;

public class TagManager : ITagManager
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    public TagManager(IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var tags = await unitOfWork.TagRepository.GetAllAsync(cancellationToken);
        return tags;
    }
}
