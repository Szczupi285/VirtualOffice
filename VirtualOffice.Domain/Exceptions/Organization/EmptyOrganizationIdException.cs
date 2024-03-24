using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class EmptyOrganizationIdException : VirtualOfficeException
    {
        public EmptyOrganizationIdException() : base("Organization Id cannot be empty")
        {
        }
    }
}
