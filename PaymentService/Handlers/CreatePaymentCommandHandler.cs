using MediatR;
using PaymentService.Api.Commands;
using PaymentService.Api.Dtos;
using PaymentService.Api.Events;
using PaymentService.Api.Queries;

namespace PaymentService.Handlers;

public class CreatePaymentCommandHandler(IMediator mediator) : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken ct)
    {
        var dto = new PaymentDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new PaymentCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentDto?>
{
    public Task<PaymentDto?> Handle(GetPaymentQuery request, CancellationToken ct)
        => Task.FromResult<PaymentDto?>(new PaymentDto(request.Id, "Sample", DateTime.UtcNow));
}

public class PaymentCreatedEventHandler : INotificationHandler<PaymentCreatedEvent>
{
    public Task Handle(PaymentCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
