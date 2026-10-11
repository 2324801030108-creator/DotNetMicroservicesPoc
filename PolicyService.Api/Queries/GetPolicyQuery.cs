using MediatR;
using PolicyService.Api.Dtos;

namespace PolicyService.Api.Queries;

public record GetPolicyQuery(Guid Id) : IRequest<PolicyDto?>;
