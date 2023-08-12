using AutoMapper;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Application
{
    public class TagDataService : ITagDataService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IMapper mapper;

        public TagDataService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetTagsAsync(CancellationToken cancellationToken = default)
        {
            using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
            var tags = await unitOfWork.GetRepository<Tag>().GetAsync(includeProperties: $"{nameof(Tag.Category)},{nameof(Tag.Lessons)}", cancellationToken: cancellationToken);
            return mapper.Map<IEnumerable<TagDto>>(tags);
        }
    }
}
