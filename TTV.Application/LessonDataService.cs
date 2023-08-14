using AutoMapper;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Domain.Specifications;
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

    public async Task<IEnumerable<LessonDto>> GetLessonsAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.GetRepository<Lesson>().GetAsync(includeProperties: $"{nameof(Lesson.Tags)}.{nameof(Tag.Category)}", cancellationToken: cancellationToken);
        return mapper.Map<IEnumerable<LessonDto>>(lessons);
    }

    public async Task<LessonDto> GetLessonAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lesson = await unitOfWork.GetRepository<Lesson>()
            .GetAsync(lessonId,
                includeProperties: $"{nameof(Lesson.Tags)}.{nameof(Tag.Category)}",
                cancellationToken: cancellationToken);
        return mapper.Map<LessonDto>(lesson);
    }


    public async Task<IEnumerable<LessonDto>> SearchLessonsAsync(SearchLessonsDto searchDto, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var lessons = await unitOfWork.GetRepository<Lesson>().GetAsync(new LessonSearchSpecification(searchDto.SearchText, searchDto.SearchTagsIds), cancellationToken: cancellationToken);
        return mapper.Map<IEnumerable<LessonDto>>(lessons);
    }
}
