using MediatR;
using AuthService.Api.Dtos;

namespace AuthService.Api.Events;

public record AuthCreatedEvent(AuthDto Item) : INotification;
