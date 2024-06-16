using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.OrganizationEvents
{
    public record OfficeAdded(Organization organization, Office office) : IDomainEvent;
}
