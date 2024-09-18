using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.PublicDocumentEvents
{
    public record PublicDocumentSettedCreationDate(PublicDocument document, ApplicationUserId userId, DocumentCreationDate date) : IDomainEvent;
}