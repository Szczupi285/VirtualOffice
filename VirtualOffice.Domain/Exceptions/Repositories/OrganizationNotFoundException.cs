using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class OrganizationNotFoundException : VirtualOfficeException
    {
        public OrganizationNotFoundException(Guid guid) : base($"Employee Task with Id: {guid} has not been found")
        {
        }
    }
}