using MediatR;
using Microsoft.AspNetCore.Mvc;
using PricingService.Api.Commands;
using PricingService.Api.Dtos;
using PricingService.Api.Queries;

namespace PricingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricingController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<PricingDto> Create(CreatePricingCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<PricingDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetPricingQuery(id), ct);
}
