using MediatR;
using DocumentService.Api.Dtos;

namespace DocumentService.Api.Commands;

public record CreateDocumentCommand(string Name) : IRequest<DocumentDto>;
