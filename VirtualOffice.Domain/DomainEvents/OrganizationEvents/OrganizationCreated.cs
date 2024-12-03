using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record OrganizationCreated(OrganizationId id, OrganizationName name,
            HashSet<ApplicationUser> organizationUsers, Subscription subscription) : IDomainEvent;
}