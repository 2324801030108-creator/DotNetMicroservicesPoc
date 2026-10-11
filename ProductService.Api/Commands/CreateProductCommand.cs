using MediatR;
using ProductService.Api.Dtos;

namespace ProductService.Api.Commands;

public record CreateProductCommand(string Name) : IRequest<ProductDto>;
