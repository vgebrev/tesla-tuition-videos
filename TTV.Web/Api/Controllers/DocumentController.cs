using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Infrastructure.Documents;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentController(ILogger<DocumentController> logger, IDocumentManager documentManager, IDocumentStreamLoader documentStreamLoader) : ControllerBase
{
    [HttpGet("lesson/{lessonId}")]
    [Authorize]
    public async Task<IEnumerable<DocumentDto>> GetDocumentsForLesson([FromRoute] int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({lessonId})", nameof(GetDocumentsForLesson), lessonId);
        var documents = await documentManager.GetDocumentsForLessonOwnedByUserAsync(lessonId, cancellationToken);
        return documents.ToDocumentDtoEnumerable();
    }

    [HttpGet("{documentId}")]
    [Authorize]
    public async Task<IResult> GetDocumentStream([FromRoute]int documentId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({documentId}", nameof(GetDocumentStream), documentId);
        var documentStreamInfo = await documentStreamLoader.LoadDocumentStreamAsync(documentId, cancellationToken);

        if (documentStreamInfo.Stream == Stream.Null)
        {
            return Results.Forbid();
        }

        return Results.File(documentStreamInfo.Stream, documentStreamInfo.ContentType, documentStreamInfo.Filename, enableRangeProcessing: true);
    }
}
