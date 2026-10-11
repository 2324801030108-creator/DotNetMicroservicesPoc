using MediatR;
using DashboardService.Api.Commands;
using DashboardService.Api.Dtos;
using DashboardService.Api.Events;
using DashboardService.Api.Queries;

namespace DashboardService.Handlers;

public class CreateDashboardCommandHandler(IMediator mediator) : IRequestHandler<CreateDashboardCommand, DashboardDto>
{
    public async Task<DashboardDto> Handle(CreateDashboardCommand request, CancellationToken ct)
    {
        var dto = new DashboardDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new DashboardCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto?>
{
    public Task<DashboardDto?> Handle(GetDashboardQuery request, CancellationToken ct)
        => Task.FromResult<DashboardDto?>(new DashboardDto(request.Id, "Sample", DateTime.UtcNow));
}

public class DashboardCreatedEventHandler : INotificationHandler<DashboardCreatedEvent>
{
    public Task Handle(DashboardCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
