using MediatR;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record UpdatePublicDocumentTitle(Guid Id, string Title) : IRequest;
}
