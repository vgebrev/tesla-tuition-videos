using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class LessonMappings
{
    public static LessonDto ToLessonDto(this Lesson lesson, DateTime? priceDate = null, Guid? ownerId = null) =>
        new()
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            LessonType = lesson.LessonType.ToLookupDto(),
            IsFree = lesson.IsFree,
            Tags = lesson.Tags.ToSimpleTagDtoEnumerable().ToArray(),
            Owner = lesson.OwnedBy.SingleOrDefault(user => ownerId == null || user.Id == ownerId)?.ToUserDto(),
            CurrentPrice = lesson.PriceAt(priceDate ?? DateTime.Now).ToPriceDto(),
            Duration = lesson.Videos.SingleOrDefault(video => video.VideoType == VideoType.FullLesson)?.Duration ?? TimeSpan.Zero
        };


    public static IEnumerable<LessonDto> ToLessonDtoEnumerable(this IEnumerable<Lesson> lessons, DateTime? priceDate = null, Guid? ownerId = null) =>
        lessons.Select(lesson => lesson.ToLessonDto(priceDate, ownerId));

}
