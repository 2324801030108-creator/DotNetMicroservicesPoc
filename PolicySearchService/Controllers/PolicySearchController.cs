using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolicySearchService.Api.Commands;
using PolicySearchService.Api.Dtos;
using PolicySearchService.Api.Queries;

namespace PolicySearchService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicySearchController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<PolicySearchDto> Create(CreatePolicySearchCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<PolicySearchDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetPolicySearchQuery(id), ct);
}
