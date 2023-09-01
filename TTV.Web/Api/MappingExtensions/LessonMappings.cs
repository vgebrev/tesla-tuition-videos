using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class LessonMappings
{
    public static LessonDto ToLessonDto(this Lesson lesson, DateTime? priceDate = null) =>
        new()
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            LessonType = lesson.LessonType.ToLookupDto(),
            Tags = lesson.Tags.ToSimpleTagDtoEnumerable().ToArray(),
            Owner = lesson.OwnedBy.SingleOrDefault()?.ToUserDto(),
            CurrentPrice = lesson.PriceAt(priceDate ?? DateTime.Now).ToPriceDto()
        };


    public static IEnumerable<LessonDto> ToLessonDtoEnumerable(this IEnumerable<Lesson> lessons, DateTime? priceDate = null) =>
        lessons.Select(lesson => lesson.ToLessonDto(priceDate));

}
