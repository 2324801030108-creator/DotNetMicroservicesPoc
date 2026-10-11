using MediatR;
using PolicySearchService.Api.Commands;
using PolicySearchService.Api.Dtos;
using PolicySearchService.Api.Events;
using PolicySearchService.Api.Queries;

namespace PolicySearchService.Handlers;

public class CreatePolicySearchCommandHandler(IMediator mediator) : IRequestHandler<CreatePolicySearchCommand, PolicySearchDto>
{
    public async Task<PolicySearchDto> Handle(CreatePolicySearchCommand request, CancellationToken ct)
    {
        var dto = new PolicySearchDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new PolicySearchCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetPolicySearchQueryHandler : IRequestHandler<GetPolicySearchQuery, PolicySearchDto?>
{
    public Task<PolicySearchDto?> Handle(GetPolicySearchQuery request, CancellationToken ct)
        => Task.FromResult<PolicySearchDto?>(new PolicySearchDto(request.Id, "Sample", DateTime.UtcNow));
}

public class PolicySearchCreatedEventHandler : INotificationHandler<PolicySearchCreatedEvent>
{
    public Task Handle(PolicySearchCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
