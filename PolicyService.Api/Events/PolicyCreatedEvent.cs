using MediatR;
using PolicyService.Api.Dtos;

namespace PolicyService.Api.Events;

public record PolicyCreatedEvent(PolicyDto Item) : INotification;
