using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record DocumentContentSetted(AbstractDocument document, DocumentContent content) : IDomainEvent;
}