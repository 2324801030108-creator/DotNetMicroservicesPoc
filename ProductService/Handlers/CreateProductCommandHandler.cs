using MediatR;
using ProductService.Api.Commands;
using ProductService.Api.Dtos;
using ProductService.Api.Events;
using ProductService.Api.Queries;

namespace ProductService.Handlers;

public class CreateProductCommandHandler(IMediator mediator) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var dto = new ProductDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new ProductCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto?>
{
    public Task<ProductDto?> Handle(GetProductQuery request, CancellationToken ct)
        => Task.FromResult<ProductDto?>(new ProductDto(request.Id, "Sample", DateTime.UtcNow));
}

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
