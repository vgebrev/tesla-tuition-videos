using TTV.Domain;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

public static class PageInfoMappings
{
    public static PageInfoDto? ToPageInfoDto(this PageInfo? pageInfo)
    {
        if (pageInfo == null)
        {
            return null;
        }

        return new PageInfoDto
        {
            Skip = pageInfo.Skip,
            Take = pageInfo.Take,
            Count = pageInfo.Count
        };
    }
}
