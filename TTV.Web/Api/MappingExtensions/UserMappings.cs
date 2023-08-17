using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class UserMappings
{
    public static UserDto? ToUserDto(this User? user)
    {
        if (user == null)
        {
            return null;
        }

        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
}
