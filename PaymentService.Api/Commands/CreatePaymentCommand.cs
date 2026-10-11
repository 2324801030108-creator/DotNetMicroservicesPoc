using MediatR;
using PaymentService.Api.Dtos;

namespace PaymentService.Api.Commands;

public record CreatePaymentCommand(string Name) : IRequest<PaymentDto>;
