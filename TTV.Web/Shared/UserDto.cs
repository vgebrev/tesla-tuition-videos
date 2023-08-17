namespace TTV.Web.Shared;

public record UserDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string? Email { get; init; } = string.Empty;
}
