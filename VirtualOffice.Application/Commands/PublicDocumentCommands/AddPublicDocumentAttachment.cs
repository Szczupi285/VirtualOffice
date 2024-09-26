using MediatR;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record AddPublicDocumentAttachment(Guid Id, string AttachmentFilePath) : IRequest;

}
