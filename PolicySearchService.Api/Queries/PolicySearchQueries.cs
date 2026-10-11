using MediatR;
using PolicySearchService.Api.Queries.Dtos;

namespace PolicySearchService.Api.Queries;

public class FindPolicyQuery : IRequest<FindPolicyResult>
{
    public string? Query { get; set; }
}

public class FindPolicyResult
{
    public List<PolicyDto> Policies { get; set; } = new();
}
