using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class InvalidEmployeeTaskEndDateException : VirtualOfficeException
    {
        DateTime Value;
        public InvalidEmployeeTaskEndDateException(DateTime value) : base($"EmployeeTask EndDate cannot be in the past")
        {
            Value = value;
        }
    }
}
