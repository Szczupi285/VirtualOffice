using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Domain.DomainEvents.EmployeeTask;
using VirtualOffice.Domain.Interfaces;

namespace VirtualOffice.Domain.Entities
{
    public class EmployeeTask : AbstractScheduleItem, IEmployeeTask, IComparable<EmployeeTask>
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

        public void SetPriority(EmployeeTaskPriorityEnum priority)
        {
            _Priority = priority;
            AddEvent(new PrioritySetted(this, priority));
        }
        public void UpdateStatus(EmployeeTaskStatusEnum Status)
        {
            _TaskStatus = Status;
            AddEvent(new StatusUpdated(this, Status));
        }

        public int CompareTo(EmployeeTask? other)
        {
            if(other == null)
                throw new ArgumentNullException();
            return other._Priority.CompareTo(_Priority);
        }
    }
}
