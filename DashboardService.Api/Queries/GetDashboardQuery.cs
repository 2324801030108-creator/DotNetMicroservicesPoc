using MediatR;
using DashboardService.Api.Dtos;

namespace DashboardService.Api.Queries;

public record GetDashboardQuery(Guid Id) : IRequest<DashboardDto?>;
