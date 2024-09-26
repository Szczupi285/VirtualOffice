using MediatR;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record DeletePublicDocument(Guid Id) : IRequest;
}
