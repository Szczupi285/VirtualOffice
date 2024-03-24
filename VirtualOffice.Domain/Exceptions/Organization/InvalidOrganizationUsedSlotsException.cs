using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class InvalidOrganizationUsedSlotsException : VirtualOfficeException
    {
        public InvalidOrganizationUsedSlotsException() : base("OrganizationUsedSlots cannot be equal to '0'.")
        {
        }
    }
}
