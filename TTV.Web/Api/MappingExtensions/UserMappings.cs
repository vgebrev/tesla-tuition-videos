using System.Runtime.CompilerServices;
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
            Name = user.Name,
            Provider = user.Provider
        };
    }

    public static IEnumerable<UserDto> ToEnumerableUserDto(this IEnumerable<User> users) =>
        users.Select(user => user.ToUserDto()!);
}
