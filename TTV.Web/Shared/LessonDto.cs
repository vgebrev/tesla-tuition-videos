namespace TTV.Web.Shared;

public record LessonDto
{
    public int Id { get; init; } 
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public LookupDto LessonType { get; init; } = new();
    public bool IsFree { get; init; } = false;
    public SimpleTagDto[] Tags { get; init; } = [];
    public UserDto? Owner { get; init; }
    public PriceDto CurrentPrice { get; init; }  = new();
    public TimeSpan Duration { get; init; } = TimeSpan.Zero;
}
