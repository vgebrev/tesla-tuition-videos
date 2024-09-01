using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Infrastructure.Documents;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("documents")]
[ApiController]
public class DocumentsController(ILogger<DocumentsController> logger, IDocumentManager documentManager, IDocumentStreamLoader documentStreamLoader) : ControllerBase
{
    [HttpGet("lesson/{lessonId}")]
    public async Task<IEnumerable<DocumentDto>> GetDocumentsForLesson([FromRoute] int lessonId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Method}({lessonId})", nameof(GetDocumentsForLesson), lessonId);
        var documents = await documentManager.GetDocumentsForLessonOwnedByUserAsync(lessonId, cancellationToken);
        return documents.ToDocumentDtoEnumerable();
    }

    [HttpGet("{documentId}")]
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
