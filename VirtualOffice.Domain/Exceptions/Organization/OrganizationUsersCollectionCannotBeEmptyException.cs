using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OrganizationUsersCollectionCannotBeEmptyException : VirtualOfficeException
    {
        public OrganizationUsersCollectionCannotBeEmptyException() : base($"Organization Users Collection cannot be empty")
        {
        }
    }
}