using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class LessonMappings
{
    public static LessonDto ToLessonDto(this Lesson lesson)
    {
        return new LessonDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            LessonType = lesson.LessonType.ToLookupDto(),
            Tags = lesson.Tags.ToSimpleTagDtoEnumerable().ToArray(),
        };
    }

    public static IEnumerable<LessonDto> ToLessonDtoEnumerable(this IEnumerable<Lesson> lessons)
    {
        return lessons.Select(ToLessonDto);
    }
}
