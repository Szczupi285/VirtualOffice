using MediatR;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record DeletePublicDocumentAttachment(Guid Id, string AttachmentFilePath) : IRequest;

}
