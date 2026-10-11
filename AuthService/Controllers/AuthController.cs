using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthService.Api.Commands;
using AuthService.Api.Dtos;
using AuthService.Api.Queries;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<AuthDto> Create(CreateAuthCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<AuthDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetAuthQuery(id), ct);
}
