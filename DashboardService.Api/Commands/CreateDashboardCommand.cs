using MediatR;
using DashboardService.Api.Dtos;

namespace DashboardService.Api.Commands;

public record CreateDashboardCommand(string Name) : IRequest<DashboardDto>;
