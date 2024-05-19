using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Consts;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.Entities
{
    public class EmployeeTask : AbstractScheduleItem, IComparable<EmployeeTask>
    {
       
        public EmployeeTaskPriorityEnum _Priority {get; private set;}
        public EmployeeTaskStatusEnum _TaskStatus{ get; private set; } = EmployeeTaskStatusEnum.NotStarted;

        public EmployeeTask(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription description,
           HashSet<ApplicationUser> assignedEmployees, EmployeeTaskPriorityEnum priority,
           ScheduleItemStartDate startDate, ScheduleItemEndDate endDate) 
            : base(id, title, description, assignedEmployees, startDate, endDate)
        {
            _Priority = priority;
        }

        public void SetPriority(EmployeeTaskPriorityEnum priority) => _Priority = priority;
        public void UpdateStatus(EmployeeTaskStatusEnum Status) => _TaskStatus = Status;

        public int CompareTo(EmployeeTask? other)
        {
            if(other == null)
                throw new ArgumentNullException();
            return other._Priority.CompareTo(_Priority);
        }
    }
}
