using MediatR;
using Microsoft.AspNetCore.Mvc;
using ChatService.Api.Commands;
using ChatService.Api.Dtos;
using ChatService.Api.Queries;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<ChatDto> Create(CreateChatCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<ChatDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetChatQuery(id), ct);
}
