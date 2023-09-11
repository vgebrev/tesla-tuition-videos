using Microsoft.Extensions.Logging;
using Stubble.Core;
using Stubble.Core.Builders;

namespace TTV.Infrastructure.Notifications.Templates;

public class TemplateRenderer : ITemplateRenderer
{
    private readonly StubbleVisitorRenderer stubble;
    private readonly ILogger<TemplateRenderer> logger;

    public TemplateRenderer(ILogger<TemplateRenderer> logger)
    {
        this.logger = logger;
        stubble = new StubbleBuilder().Build();
    }

    public async Task<string?> RenderAsync<TData>(TData data, string? templateFile = null, CancellationToken cancellationToken = default)
    {
        templateFile ??= $"{typeof(TData).FullName}.mustache";
        logger.LogDebug("Rendering {TemplateFile} with data {@Data}", templateFile, data);
        using var stream = typeof(TemplateRenderer).Assembly.GetManifestResourceStream(templateFile);
        if (stream == null)
        {
            logger.LogError("Embedded resource for template not found");
            throw new ApplicationException($"Template file {templateFile} not found");
        }
        var template = await new StreamReader(stream).ReadToEndAsync(cancellationToken);
        var result = await stubble.RenderAsync(template, data);
        return result;
    }
}
