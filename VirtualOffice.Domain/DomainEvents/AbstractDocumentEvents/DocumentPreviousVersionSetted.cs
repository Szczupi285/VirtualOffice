using VirtualOffice.Domain.Abstractions;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record DocumentPreviousVersionSetted(AbstractDocument document, AbstractDocument previousVersion) : IDomainEvent;
}