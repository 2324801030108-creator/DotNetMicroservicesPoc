using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolicyService.Api.Commands;
using PolicyService.Api.Dtos;
using PolicyService.Api.Queries;

namespace PolicyService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController(IMediator mediator) : ControllerBase
{
    [HttpPost("offers")]
    public Task<CreateOfferResult> CreateOffer(CreateOfferCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpPost("policies")]
    public Task<CreatePolicyResult> Create(CreatePolicyCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<PolicyDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetPolicyQuery(id), ct);

    [HttpGet("details/{policyNumber}")]
    public Task<GetPolicyDetailsQueryResult> GetDetails(string policyNumber, CancellationToken ct)
        => mediator.Send(new GetPolicyDetailsQuery { PolicyNumber = policyNumber }, ct);
}
