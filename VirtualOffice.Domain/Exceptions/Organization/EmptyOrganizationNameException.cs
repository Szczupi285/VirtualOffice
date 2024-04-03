using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class EmptyOrganizationNameException : VirtualOfficeException
    {
        public EmptyOrganizationNameException() : base("Organization name cannot be empty")
        {
        }
    }
}
