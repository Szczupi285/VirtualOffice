using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Entities
{
    public class CalendarEvent : AbstractScheduleItem
    {
        public CalendarEvent(ScheduleItemId id, ScheduleItemTitle titile, ScheduleItemDescription eventDescription, 
            HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
            : base(id, titile, eventDescription, assignedEmployees, startDate, endDate)
        {
        }

        public void UpdateStartDate(DateTime startDate)
        {
            if (_EndDate <= startDate)
                throw new EndDateCannotBeBeforeStartDate(_EndDate, startDate);

            _StartDate = startDate;
        }

      
    }
}
