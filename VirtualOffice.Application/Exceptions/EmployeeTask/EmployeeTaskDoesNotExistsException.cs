using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.EmployeeTask
{
    public class EmployeeTaskDoesNotExistsException : VirtualOfficeException
    {
        public EmployeeTaskDoesNotExistsException(Guid id) : base($"EmployeeTask with id: {id} does not exists")
        {
        }
    }
}
