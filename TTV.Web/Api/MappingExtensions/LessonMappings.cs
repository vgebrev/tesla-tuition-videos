using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class LessonMappings
{
    public static LessonDto ToLessonDto(this Lesson lesson) =>
        new()
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            LessonType = lesson.LessonType.ToLookupDto(),
            Tags = lesson.Tags.ToSimpleTagDtoEnumerable().ToArray(),
            Owner = lesson.OwnedBy.SingleOrDefault()?.ToUserDto(),
            CurrentPrice = lesson.CurrentPrice.ToPriceDto()
        };


    public static IEnumerable<LessonDto> ToLessonDtoEnumerable(this IEnumerable<Lesson> lessons) =>
        lessons.Select(ToLessonDto);

}
