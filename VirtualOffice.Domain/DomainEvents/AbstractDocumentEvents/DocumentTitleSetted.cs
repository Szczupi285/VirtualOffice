using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record DocumentTitleSetted(AbstractDocument document, DocumentTitle title) : IDomainEvent;
}