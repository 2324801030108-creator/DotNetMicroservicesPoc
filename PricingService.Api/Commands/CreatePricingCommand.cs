using MediatR;
using PricingService.Api.Dtos;

namespace PricingService.Api.Commands;

public record CreatePricingCommand(string Name) : IRequest<PricingDto>;
