using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class TagCategoryMappings
{
    public static TagCategoryDto ToCategoryDto(this TagCategory category)
    {
        return new TagCategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
