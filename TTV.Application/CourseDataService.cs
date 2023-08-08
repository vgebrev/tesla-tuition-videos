using AutoMapper;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Application;

public class CourseDataService : ICourseDataService
{
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IMapper mapper;

    public CourseDataService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var courses = await unitOfWork.GetRepository<Course>().GetAsync();
        return mapper.Map<IEnumerable<CourseDto>>(courses);
    }
}
