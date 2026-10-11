using MediatR;
using DashboardService.Api.Dtos;

namespace DashboardService.Api.Events;

public record DashboardCreatedEvent(DashboardDto Item) : INotification;
