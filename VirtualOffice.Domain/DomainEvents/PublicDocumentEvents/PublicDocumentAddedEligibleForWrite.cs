using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.DomainEvents.PublicDocumentEvents
{
    public record PublicDocumentAddedEligibleForWrite(PublicDocument document, ApplicationUserId userId) : IDomainEvent;
}