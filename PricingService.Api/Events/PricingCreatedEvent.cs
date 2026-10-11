using MediatR;
using PricingService.Api.Dtos;

namespace PricingService.Api.Events;

public record PricingCreatedEvent(PricingDto Item) : INotification;
