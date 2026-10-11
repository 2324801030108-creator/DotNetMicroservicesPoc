using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Commands;
using ProductService.Api.Dtos;
using ProductService.Api.Queries;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<ProductDto> Create(CreateProductCommand cmd, CancellationToken ct)
        => mediator.Send(cmd, ct);

    [HttpGet("{id:guid}")]
    public Task<ProductDto?> Get(Guid id, CancellationToken ct)
        => mediator.Send(new GetProductQuery(id), ct);
}
