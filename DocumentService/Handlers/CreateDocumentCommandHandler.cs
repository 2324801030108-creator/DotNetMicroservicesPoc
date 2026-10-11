using MediatR;
using DocumentService.Api.Commands;
using DocumentService.Api.Dtos;
using DocumentService.Api.Events;
using DocumentService.Api.Queries;

namespace DocumentService.Handlers;

public class CreateDocumentCommandHandler(IMediator mediator) : IRequestHandler<CreateDocumentCommand, DocumentDto>
{
    public async Task<DocumentDto> Handle(CreateDocumentCommand request, CancellationToken ct)
    {
        var dto = new DocumentDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new DocumentCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentDto?>
{
    public Task<DocumentDto?> Handle(GetDocumentQuery request, CancellationToken ct)
        => Task.FromResult<DocumentDto?>(new DocumentDto(request.Id, "Sample", DateTime.UtcNow));
}

public class DocumentCreatedEventHandler : INotificationHandler<DocumentCreatedEvent>
{
    public Task Handle(DocumentCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
