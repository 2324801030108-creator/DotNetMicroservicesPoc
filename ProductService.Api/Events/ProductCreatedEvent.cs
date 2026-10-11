using MediatR;
using ProductService.Api.Dtos;

namespace ProductService.Api.Events;

public record ProductCreatedEvent(ProductDto Item) : INotification;
