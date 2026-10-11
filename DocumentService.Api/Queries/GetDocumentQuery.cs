using MediatR;
using DocumentService.Api.Dtos;

namespace DocumentService.Api.Queries;

public record GetDocumentQuery(Guid Id) : IRequest<DocumentDto?>;
