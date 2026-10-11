using MediatR;
using PaymentService.Api.Dtos;

namespace PaymentService.Api.Queries;

public record GetPaymentQuery(Guid Id) : IRequest<PaymentDto?>;
