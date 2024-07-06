using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record UserAddedToOrganization(Organization organization, ApplicationUser user) : IDomainEvent;
}
