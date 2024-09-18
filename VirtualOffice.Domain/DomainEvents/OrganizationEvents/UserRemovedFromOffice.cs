using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record UserRemovedFromOffice(Organization organization, Office office, ApplicationUser User) : IDomainEvent;
}