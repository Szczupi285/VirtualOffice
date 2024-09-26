using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.EmployeeTask
{
    public class EmployeeTaskAlreadyExistsException : VirtualOfficeException
    {
        public EmployeeTaskAlreadyExistsException(Guid guid) : base($"Employee task with id: {guid} already exists")
        {
        }
    }
}
