using MediatR;
using DocumentService.Api.Dtos;

namespace DocumentService.Api.Events;

public record DocumentCreatedEvent(DocumentDto Item) : INotification;
