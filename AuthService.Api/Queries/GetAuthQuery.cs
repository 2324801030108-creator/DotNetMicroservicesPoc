using MediatR;
using AuthService.Api.Dtos;

namespace AuthService.Api.Queries;

public record GetAuthQuery(Guid Id) : IRequest<AuthDto?>;
