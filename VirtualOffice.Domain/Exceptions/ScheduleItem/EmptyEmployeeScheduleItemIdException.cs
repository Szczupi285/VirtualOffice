using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class EmptyEmployeeScheduleItemIdException : VirtualOfficeException
    {
        public EmptyEmployeeScheduleItemIdException() 
            : base("ScheduleItem Id cannot be empty")
        {
        }
    }
}
