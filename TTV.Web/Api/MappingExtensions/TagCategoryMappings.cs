using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class TagCategoryMappings
{
    public static TagCategoryDto ToCategoryDto(this TagCategory category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name
        };
}
