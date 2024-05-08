using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class EmptyEmployeeTaskDescriptionException : VirtualOfficeException
    {
        public EmptyEmployeeTaskDescriptionException() : base("EmployeeTask Description cannot be empty")
        {
        }
    }
}
