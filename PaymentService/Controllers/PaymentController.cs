using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Api.Commands;
using PaymentService.Api.Dtos;
using PaymentService.Api.Queries;

namespace PaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<PaymentDto> Create(CreatePaymentCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<PaymentDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetPaymentQuery(id), ct);
}
