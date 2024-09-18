using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record AttachmentDeleted(AbstractDocument document, DocumentFilePath filePath) : IDomainEvent;
}