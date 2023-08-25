namespace TTV.Web.Shared;

public record OrderDto
{
    public int Id { get; init; }
    public LessonDto[] Lessons { get; init; } = Array.Empty<LessonDto>();
    public UserDto PlacedBy { get; init; } = new();
    public DateTime PlacedOn { get; init; }
    public LookupDto Status { get; init; } = new();
    public string? StatusReason { get; init; }
    public decimal TotalAmount { get; init; }
    public AppliedDiscountDto[] AppliedDiscounts { get; init; } = Array.Empty<AppliedDiscountDto>();
    public bool CanCheckout { get; set; }
}
