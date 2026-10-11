using MediatR;
using PolicyService.Api.Commands;
using PolicyService.Api.Dtos;
using PolicyService.Api.Events;
using PolicyService.Api.Queries;

namespace PolicyService.Handlers;

public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, CreateOfferResult>
{
    private readonly IMediator _mediator;
    public CreateOfferHandler(IMediator mediator) => _mediator = mediator;
    public async Task<CreateOfferResult> Handle(CreateOfferCommand request, CancellationToken ct)
    {
        var result = new CreateOfferResult { OfferNumber = Guid.NewGuid().ToString("N"), TotalPrice = 1000 };
        await _mediator.Publish(new PolicyCreatedEvent(new PolicyDto(Guid.NewGuid(), request.ProductCode, DateTime.UtcNow)), ct);
        return result;
    }
}

public class CreatePolicyHandler : IRequestHandler<CreatePolicyCommand, CreatePolicyResult>
{
    public Task<CreatePolicyResult> Handle(CreatePolicyCommand request, CancellationToken ct)
        => Task.FromResult(new CreatePolicyResult { PolicyNumber = Guid.NewGuid().ToString("N") });
}

public class GetPolicyQueryHandler : IRequestHandler<GetPolicyQuery, PolicyDto?>
{
    public Task<PolicyDto?> Handle(GetPolicyQuery request, CancellationToken ct)
        => Task.FromResult<PolicyDto?>(new PolicyDto(request.Id, "Sample", DateTime.UtcNow));
}

public class GetPolicyDetailsHandler : IRequestHandler<GetPolicyDetailsQuery, GetPolicyDetailsQueryResult>
{
    public Task<GetPolicyDetailsQueryResult> Handle(GetPolicyDetailsQuery request, CancellationToken ct)
        => Task.FromResult(new GetPolicyDetailsQueryResult
        {
            Policy = new PolicyDetailsDto
            {
                Number = request.PolicyNumber,
                DateFrom = DateTime.UtcNow,
                DateTo = DateTime.UtcNow.AddYears(1),
                PolicyHolder = "Sample Holder",
                TotalPremium = 1000,
                ProductCode = "SAMPLE",
                Covers = new List<string> { "C1" }
            }
        });
}

public class PolicyCreatedEventHandler : INotificationHandler<PolicyCreatedEvent>
{
    public Task Handle(PolicyCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
