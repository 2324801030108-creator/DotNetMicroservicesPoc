using MediatR;
using ProductService.Api.Dtos;

namespace ProductService.Api.Queries;

public record GetProductQuery(Guid Id) : IRequest<ProductDto?>;
