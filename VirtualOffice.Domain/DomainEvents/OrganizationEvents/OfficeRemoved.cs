using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record OfficeRemoved(Organization organization, Office office) : IDomainEvent;

}
