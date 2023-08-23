using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class TagMappings
{
    public static TagDto ToTagDto(this Tag tag) =>
        new()
        {
            Id = tag.Id,
            Name = tag.Name,
            Category = tag.Category.ToCategoryDto(),
            LessonCount = tag.Lessons.Count
        };

    public static IEnumerable<TagDto> ToTagDtoEnumerable(this IEnumerable<Tag> tags) =>
        tags.Select(ToTagDto);

    public static SimpleTagDto ToSimpleTagDto(this Tag tag) =>
        new()
        {
            Name = tag.Name,
            Priority = tag.Category.Priority
        };

    public static IEnumerable<SimpleTagDto> ToSimpleTagDtoEnumerable(this IEnumerable<Tag> tags) =>
        tags.Select(ToSimpleTagDto);
}
