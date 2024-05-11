using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class EmployeeTaskStartDateCannotBePastException : VirtualOfficeException
    {
        DateTime _startDate;
        public EmployeeTaskStartDateCannotBePastException(DateTime startDate) : base($"EmployeeTaskStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}
