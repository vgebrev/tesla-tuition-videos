using AutoMapper;
using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Lesson, LessonDto>();
        CreateMap<LessonType, LookupDto>()
            .ConvertUsing<EnumToLookupDtoConverter<LessonType>>();
    }
}

