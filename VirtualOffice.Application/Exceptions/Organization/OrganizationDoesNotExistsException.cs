using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Organization
{
    public class OrganizationDoesNotExistsException : VirtualOfficeException
    {
        public OrganizationDoesNotExistsException(Guid id) : base($"Organization with Id: {id} does not exist")
        {
        }
    }
}
