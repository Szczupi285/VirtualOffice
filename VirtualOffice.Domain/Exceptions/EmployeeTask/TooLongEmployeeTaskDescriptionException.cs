using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class TooLongEmployeeTaskDescriptionException : VirtualOfficeException
    {
        string Value;
        public TooLongEmployeeTaskDescriptionException(string value, uint length) : base($"EmployeeTask Description: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
