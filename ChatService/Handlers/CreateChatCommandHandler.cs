using MediatR;
using ChatService.Api.Commands;
using ChatService.Api.Dtos;
using ChatService.Api.Events;
using ChatService.Api.Queries;

namespace ChatService.Handlers;

public class CreateChatCommandHandler(IMediator mediator) : IRequestHandler<CreateChatCommand, ChatDto>
{
    public async Task<ChatDto> Handle(CreateChatCommand request, CancellationToken ct)
    {
        var dto = new ChatDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new ChatCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetChatQueryHandler : IRequestHandler<GetChatQuery, ChatDto?>
{
    public Task<ChatDto?> Handle(GetChatQuery request, CancellationToken ct)
        => Task.FromResult<ChatDto?>(new ChatDto(request.Id, "Sample", DateTime.UtcNow));
}

public class ChatCreatedEventHandler : INotificationHandler<ChatCreatedEvent>
{
    public Task Handle(ChatCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
