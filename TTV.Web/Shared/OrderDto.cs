namespace TTV.Web.Shared;

public record OrderDto
{
    public int Id { get; init; }
    public LessonDto[] Lessons { get; init; } = [];
    public UserDto PlacedBy { get; init; } = new();
    public DateTime PlacedOn { get; init; }
    public LookupDto Status { get; init; } = new();
    public string? StatusReason { get; init; }
    public decimal TotalAmount { get; init; }
    public AppliedDiscountDto[] AppliedDiscounts { get; init; } = [];
    public PaymentDto[] Payments { get; init; } = [];
    public bool HasOwnedLessons { get; init; }
    public bool IsFinalised { get; init; }
    public bool IsPayable { get; init; }
    public bool CanComplete { get; init; }
}
