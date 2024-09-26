using MediatR;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record UpdatePublicDocumentContent(Guid Id, string Content) : IRequest;
}
