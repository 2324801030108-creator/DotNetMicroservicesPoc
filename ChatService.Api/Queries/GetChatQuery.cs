using System;
using MediatR;
using ChatService.Api.Dtos;

namespace ChatService.Api.Queries;

public record GetChatQuery(Guid Id) : IRequest<ChatDto?>;
