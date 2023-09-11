namespace TTV.Infrastructure.Notifications.Templates;

public interface ITemplateRenderer
{
    Task<string?> RenderAsync<TData>(TData data, string? templateFile = null, CancellationToken cancellationToken = default);
}