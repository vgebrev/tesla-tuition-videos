namespace TTV.Web.Shared;

public record UserDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Provider { get; init; } = string.Empty;
    public string? Email { get; init; } = string.Empty;
}
