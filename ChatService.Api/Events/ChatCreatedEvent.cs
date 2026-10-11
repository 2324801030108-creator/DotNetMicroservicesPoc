using MediatR;
using ChatService.Api.Dtos;

namespace ChatService.Api.Events;

public record ChatCreatedEvent(ChatDto Item) : INotification;
