using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.DomainEvents.PublicDocumentEvents
{
    public record PublicDocumentAddedEligibleForRead(PublicDocument document, ApplicationUserId userId) : IDomainEvent;
}