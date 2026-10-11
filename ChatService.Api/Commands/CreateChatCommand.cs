using MediatR;
using ChatService.Api.Dtos;

namespace ChatService.Api.Commands;

public record CreateChatCommand(string Name) : IRequest<ChatDto>;
