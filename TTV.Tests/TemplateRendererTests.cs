using Shouldly;
using Microsoft.Extensions.Logging.Abstractions;
using TTV.Infrastructure.Notifications.Templates;

namespace TTV.Tests;
public class TemplateRendererTests
{
    [Fact]
    public async Task OrderNotificationRender()
    {
        TemplateRenderer renderer = new(new NullLogger<TemplateRenderer>());
        var items = new OrderItem[] { new(1, "Item 1", 10), new(2, "Item 2", 20) };
        var order = new OrderConfirmationTemplate(1, "John Doe", 30, items);

        var result = await renderer.RenderAsync(order);

        result.ShouldNotBeNullOrEmpty();
    }
}
