using MediatR;
using PolicySearchService.Api.Dtos;

namespace PolicySearchService.Api.Commands;

public record CreatePolicySearchCommand(string Name) : IRequest<PolicySearchDto>;
