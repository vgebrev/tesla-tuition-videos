using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TTV.Application;

namespace TTV.Web.Api.Controllers;
[Route("ai-tutor")]
[ApiController]
public class AiTutorController(IAiTutor aiTutor) : ControllerBase
{
    private readonly IAiTutor aiTutor = aiTutor;

    [HttpPost("ask")]
    [Authorize]
    public async Task Ask([FromBody] AiTutorQuestion question, CancellationToken cancellationToken = default)
    {
        Response.ContentType = "text/plain";
        
        await foreach (var message in aiTutor.AskAsync(question.UserInput, cancellationToken))
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await Response.Body.WriteAsync(bytes, cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
    }

    public record AiTutorQuestion(string UserInput);
}
