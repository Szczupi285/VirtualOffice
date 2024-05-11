using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class EmployeeTaskEndDateCannotBeBeforeStartDate : VirtualOfficeException
    {
        DateTime EndDate;
        DateTime StartDate;
        public EmployeeTaskEndDateCannotBeBeforeStartDate(DateTime endDate, DateTime startDate) : base($"End Date: {endDate} is before Start Date: {startDate}")
        {
            EndDate = endDate;
            StartDate = startDate;
        }
    }
}
