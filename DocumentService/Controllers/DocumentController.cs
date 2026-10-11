using MediatR;
using Microsoft.AspNetCore.Mvc;
using DocumentService.Api.Commands;
using DocumentService.Api.Dtos;
using DocumentService.Api.Queries;

namespace DocumentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<DocumentDto> Create(CreateDocumentCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<DocumentDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetDocumentQuery(id), ct);
}
