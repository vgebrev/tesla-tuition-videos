using AutoMapper;
using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Course, CourseDto>();
    }
}

