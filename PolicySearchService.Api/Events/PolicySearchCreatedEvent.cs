using MediatR;
using PolicySearchService.Api.Dtos;

namespace PolicySearchService.Api.Events;

public record PolicySearchCreatedEvent(PolicySearchDto Item) : INotification;
