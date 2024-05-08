using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class EmptyEmployeeTaskId : VirtualOfficeException
    {
        public EmptyEmployeeTaskId() 
            : base("EmployeeTask Id cannot be empty")
        {
        }
    }
}
