using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;

public record ConfirmOrderRequest
{
    public LessonDto[] Lessons { get; init; } = Array.Empty<LessonDto>();
}

public record ConfirmOrderResponse
{
    public OrderDto Order { get; init; } = new();
}

public record ConfirmOrderError
{
    public string ErrorMessage { get; init; } = string.Empty;
}

