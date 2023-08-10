using AutoMapper;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Application;

public class LessonDataService : ILessonDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IMapper mapper;

    public LessonDataService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<LessonDto>> GetLessonsAsync()
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var lessons = await unitOfWork.GetRepository<Lesson>().GetAsync();
        return mapper.Map<IEnumerable<LessonDto>>(lessons);
    }
}
