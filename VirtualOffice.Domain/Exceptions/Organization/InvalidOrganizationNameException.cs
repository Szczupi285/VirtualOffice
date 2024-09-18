using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class InvalidOrganizationNameException : VirtualOfficeException
    {
        private string Value;

        public InvalidOrganizationNameException(string value)
            : base($"Organizataion name: {value} is more than 100 characters long")
        {
            Value = value;
        }
    }
}