using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OrganizationNotEnoughSlotsException : VirtualOfficeException
    {
        public OrganizationNotEnoughSlotsException() : base($"Current Subscription does not support that many slots")
        {
        }
    }
}