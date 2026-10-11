using MediatR;
using PolicySearchService.Api.Dtos;

namespace PolicySearchService.Api.Queries;

public record GetPolicySearchQuery(Guid Id) : IRequest<PolicySearchDto?>;
