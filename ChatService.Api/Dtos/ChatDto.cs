using System;

namespace ChatService.Api.Dtos;

public record ChatDto(Guid Id, string Name, DateTime CreatedAt);
