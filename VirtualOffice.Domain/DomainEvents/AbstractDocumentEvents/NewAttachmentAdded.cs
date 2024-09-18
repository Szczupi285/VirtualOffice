using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record NewAttachmentAdded(AbstractDocument document, DocumentFilePath filePath) : IDomainEvent;
}