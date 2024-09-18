using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record UserAddedToOffice(Organization organization, Office office, ApplicationUser User) : IDomainEvent;
}