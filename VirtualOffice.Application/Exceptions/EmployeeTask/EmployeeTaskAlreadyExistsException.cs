using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
