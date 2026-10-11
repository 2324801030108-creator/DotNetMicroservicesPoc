using MediatR;
using PaymentService.Api.Dtos;

namespace PaymentService.Api.Events;

public record PaymentCreatedEvent(PaymentDto Item) : INotification;
