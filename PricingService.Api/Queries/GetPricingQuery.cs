using MediatR;
using PricingService.Api.Dtos;

namespace PricingService.Api.Queries;

public record GetPricingQuery(Guid Id) : IRequest<PricingDto?>;
