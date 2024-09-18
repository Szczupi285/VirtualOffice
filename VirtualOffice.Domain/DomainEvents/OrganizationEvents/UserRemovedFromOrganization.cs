using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record UserRemovedFromOrganization(Organization organization, ApplicationUser user) : IDomainEvent;
}