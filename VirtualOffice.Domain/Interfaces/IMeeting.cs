using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IMeeting
    {
        HashSet<ApplicationUser> _AssignedEmployees { get; }
        ScheduleItemDescription _Description { get; }
        ScheduleItemEndDate _EndDate { get; }
        ScheduleItemStartDate _StartDate { get; }
        ScheduleItemTitle _Title { get; }

        void AddEmployee(ApplicationUser user);
        void AddEmployeesRange(ICollection<ApplicationUser> users);
        void RemoveEmployee(ApplicationUser user);
        void RemoveEmployeesRange(ICollection<ApplicationUser> users);
        void SetDescription(string description);
        void SetTitle(string title);
        void UpdateEndDate(DateTime endDate);
        void UpdateStartDate(DateTime newStartDate);
        void RescheduleMeeting(DateTime newStartDate, DateTime newEndDate);
    }
}