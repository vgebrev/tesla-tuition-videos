namespace TTV.Application;
public interface IAiTutor
{
    IAsyncEnumerable<string> AskAsync(string prompt, CancellationToken cancellationToken = default);
}
