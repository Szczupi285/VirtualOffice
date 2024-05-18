using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Entities
{
    public class Meeting : AbstractScheduleItem
    {
        public Meeting(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription description,
            HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
            : base(id, title, description, assignedEmployees, startDate, endDate)
        {
        }

        public void UpdateStartDate(DateTime newStartDate)
        {
            if (newStartDate > _EndDate)
                throw new EndDateCannotBeBeforeStartDate(newStartDate, _EndDate);
        }
        public void RescheduleMeeting(DateTime newStartDate, DateTime newEndDate)
        {
            if(newStartDate > newEndDate)
                throw new EndDateCannotBeBeforeStartDate(newStartDate, newEndDate);
            _EndDate = newEndDate;
            _StartDate = newStartDate;

        }
    }
}
