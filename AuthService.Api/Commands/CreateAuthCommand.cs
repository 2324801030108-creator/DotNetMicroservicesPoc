using MediatR;
using AuthService.Api.Dtos;

namespace AuthService.Api.Commands;

public record CreateAuthCommand(string Name) : IRequest<AuthDto>;
