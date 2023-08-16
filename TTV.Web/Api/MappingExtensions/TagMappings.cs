using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class TagMappings
{
    public static TagDto ToTagDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Category = tag.Category.ToCategoryDto(),
            LessonCount = tag.Lessons.Count
        };
    }

    public static IEnumerable<TagDto> ToTagDtoEnumerable(this IEnumerable<Tag> tags)
    {
        return tags.Select(ToTagDto);
    }

    public static SimpleTagDto ToSimpleTagDto(this Tag tag)
    {
        return new SimpleTagDto()
        {
            Name = tag.Name,
            Priority = tag.Category.Priority
        };
    }

    public static IEnumerable<SimpleTagDto> ToSimpleTagDtoEnumerable(this IEnumerable<Tag> tags)
    {
        return tags.Select(ToSimpleTagDto);
    }
}
