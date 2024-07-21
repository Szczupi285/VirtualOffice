using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IAbstractScheduleItem
    {
        HashSet<ApplicationUser> _AssignedEmployees { get; }
        ScheduleItemDescription _Description { get; }
        ScheduleItemEndDate _EndDate { get; }
        ScheduleItemStartDate _StartDate { get; }
        ScheduleItemTitle _Title { get; }
        EmployeeTaskPriorityEnum _Priority { get; }
        EmployeeTaskStatusEnum _TaskStatus { get; }

        void AddEmployee(ApplicationUser user);
        void AddEmployeesRange(ICollection<ApplicationUser> users);
        void RemoveEmployee(ApplicationUser user);
        void RemoveEmployeesRange(ICollection<ApplicationUser> users);
        void SetDescription(string description);
        void SetTitle(string title);
        void UpdateEndDate(DateTime endDate);
        public void SetPriority(EmployeeTaskPriorityEnum priority);
        public void UpdateStatus(EmployeeTaskStatusEnum Status);


    }
}