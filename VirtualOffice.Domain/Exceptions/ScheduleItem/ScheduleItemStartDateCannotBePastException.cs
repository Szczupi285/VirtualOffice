using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class ScheduleItemStartDateCannotBePastException : VirtualOfficeException
    {
        DateTime _startDate;
        public ScheduleItemStartDateCannotBePastException(DateTime startDate) : base($"ScheduleItemStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}
