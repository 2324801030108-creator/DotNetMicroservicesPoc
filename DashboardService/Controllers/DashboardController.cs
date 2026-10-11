using MediatR;
using Microsoft.AspNetCore.Mvc;
using DashboardService.Api.Commands;
using DashboardService.Api.Dtos;
using DashboardService.Api.Queries;

namespace DashboardService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<DashboardDto> Create(CreateDashboardCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<DashboardDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetDashboardQuery(id), ct);
}
