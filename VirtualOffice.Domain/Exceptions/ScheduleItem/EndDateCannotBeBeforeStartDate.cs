using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class EndDateCannotBeBeforeStartDate : VirtualOfficeException
    {
        DateTime EndDate;
        DateTime StartDate;
        public EndDateCannotBeBeforeStartDate(DateTime startDate, DateTime endDate) : base($"End Date: {endDate} is before Start Date: {startDate}")
        {
            EndDate = endDate;
            StartDate = startDate;
        }
    }
}
