using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class InvalidScheduleItemEndDateException : VirtualOfficeException
    {
        DateTime Value;
        public InvalidScheduleItemEndDateException(DateTime value) : base($"ScheduleItem EndDate cannot be in the past")
        {
            Value = value;
        }
    }
}
