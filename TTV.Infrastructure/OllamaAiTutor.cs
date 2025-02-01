using Microsoft.Extensions.Options;
using OllamaSharp;
using OllamaSharp.Models;
using System.Runtime.CompilerServices;
using TTV.Application;

namespace TTV.Infrastructure;
public class OllamaAiTutor(IOptionsSnapshot<SystemSettings> config) : IAiTutor
{
    private readonly OllamaAiTutorSettings settings = config.Value.OllamaAiTutorSettings;
    private readonly OllamaApiClient client = new(config.Value.OllamaAiTutorSettings.Uri, config.Value.OllamaAiTutorSettings.Model);

    public async IAsyncEnumerable<string> AskAsync(string prompt, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!settings.IsEnabled)
        {
            yield return "This feature is not currently enabled";
        }
        else
        {
            var request = new GenerateRequest { Model = settings.Model, Prompt = prompt, Stream = true };

            await foreach (var chunk in client.GenerateAsync(request, cancellationToken))
            {
                if (chunk is null)
                {
                    continue;
                }
                yield return chunk.Response;
            }
        }
    }
}
