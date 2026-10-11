using MediatR;
using AuthService.Api.Commands;
using AuthService.Api.Dtos;
using AuthService.Api.Events;
using AuthService.Api.Queries;

namespace AuthService.Handlers;

public class CreateAuthCommandHandler(IMediator mediator) : IRequestHandler<CreateAuthCommand, AuthDto>
{
    public async Task<AuthDto> Handle(CreateAuthCommand request, CancellationToken ct)
    {
        var dto = new AuthDto(Guid.NewGuid(), request.Name, DateTime.UtcNow);
        await mediator.Publish(new AuthCreatedEvent(dto), ct);
        return dto;
    }
}

public class GetAuthQueryHandler : IRequestHandler<GetAuthQuery, AuthDto?>
{
    public Task<AuthDto?> Handle(GetAuthQuery request, CancellationToken ct)
        => Task.FromResult<AuthDto?>(new AuthDto(request.Id, "Sample", DateTime.UtcNow));
}

public class AuthCreatedEventHandler : INotificationHandler<AuthCreatedEvent>
{
    public Task Handle(AuthCreatedEvent e, CancellationToken ct) => Task.CompletedTask;
}
