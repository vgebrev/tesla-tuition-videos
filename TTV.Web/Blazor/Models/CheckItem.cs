namespace TTV.Web.Blazor.Models;

public record CheckItem<TItem>
{
    public bool IsChecked { get; set; }
    public TItem Item { get; init; } = default!;
}
