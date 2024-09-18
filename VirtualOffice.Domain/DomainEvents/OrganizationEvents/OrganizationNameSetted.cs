using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record OrganizationNameSetted(Organization organization, OrganizationName name) : IDomainEvent;
}