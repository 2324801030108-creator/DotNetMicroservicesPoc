using MediatR;
using PricingService.Api.Commands;
using PricingService.Api.Dtos;
using PricingService.Api.Events;
using PricingService.Api.Queries;

namespace PricingService.Handlers;

public class CreatePricingCommandHandler(IMediator mediator) : IRequestHandler<CreatePricingCommand, PricingDto>
{
    public async Task<PricingDto> Handle(CreatePricingCommand request, CancellationToken ct)
    {
        var dto = new PricingDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new PricingCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetPricingQueryHandler : IRequestHandler<GetPricingQuery, PricingDto?>
{
    public Task<PricingDto?> Handle(GetPricingQuery request, CancellationToken ct)
        => Task.FromResult<PricingDto?>(new PricingDto(request.Id, "Sample", DateTime.UtcNow));
}

public class PricingCreatedEventHandler : INotificationHandler<PricingCreatedEvent>
{
    public Task Handle(PricingCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
