using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class EmployeeTaskNotFoundException : VirtualOfficeException
    {
        public EmployeeTaskNotFoundException(Guid guid) : base($"Employee Task with Id: {guid} has not been found")
        {
        }
    }
}